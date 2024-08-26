using MossadAPI.Models;
using MossadAPI.Services.DTO.Missions;
using MossadAPI.Services.Interfaces;
using MossadAPI.Services.Utilities;
using MossadAPI.Data;
using MossadAPI.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MossadAPI.Services.Implementation
{
    public class MissionService : IMissionService
    {
        private readonly MossadAPIContext _context;
        public MissionService(MossadAPIContext context)
        {
            _context = context;
        }
        public async Task ControlMissions()
        {
            List<Mission> missions = _context.Missions
                .Include(m => m.Target)
                .ThenInclude(t => t.Location)
                .Include(m => m.Agent)
                .ThenInclude(a  => a.Location)
                .Where(m => m.Status == MissionStatus.InMission)
                .ToList();
            foreach (var mission in missions)
            {
                if (MissionUtilities.CalculateDistance(mission.Target.Location, mission.Agent.Location) < 1)
                {
                    UpdateStatus(mission);
                }
                else
                {
                    UpdateMovement(mission);
                }
                int timeRemaining = MissionUtilities.CalculateTime(mission.Target.Location, mission.Agent.Location);
                mission.TimeLeft = timeRemaining;
                _context.Missions.Update(mission);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mission>> GetMissions()
        {
            List<Mission> missions = _context.Missions
                .Include(m => m.Target)
                .Include(m => m.Agent)
                .Where(m => m.Status == MissionStatus.OnOffer)
                .ToList();
            if (missions == null)
            {
                throw new ArgumentNullException(nameof(missions));
            }
            missions = await IsRelevant(missions);
            missions = await IsNew(missions);
            return missions;
        }

        public async Task UpdateMissionStatus(int id, UpdateStatusDTO missionDTO)
        {
            Mission? mission = await _context.Missions
                .Include(m  => m.Target)
                .Include(m => m.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mission == null)
            {
                throw new ArgumentNullException(nameof(mission));
            }
            else if (missionDTO.Status != "assigned")
            {
                throw new InvalidOperationException("Status Invalid");
            }
            else if (!IsInDistance(mission.Target, mission.Agent))
            {
                _context.Missions.Remove(mission);
                await _context.SaveChangesAsync();
                throw new InvalidOperationException("Target Too Far");
            }
            mission.Status = MissionStatus.InMission;
            await DeleteAllMissions(mission.Target.Id, mission.Agent.Id);
            await _context.SaveChangesAsync();
        }

        public void UpdateStatus(Mission mission)
        {
            mission.Status = MissionStatus.Done;
            mission.Agent.Status = AgentStatus.InActive;
            mission.Target.Status = TargetStatus.Dead;
        }

        public void UpdateMovement(Mission mission)
        {
            Dictionary<string, int> movements = MissionUtilities.CalculateMovement(mission.Target.Location, mission.Agent.Location);
            mission.Agent.Location.X += movements["x"];
            mission.Agent.Location.Y += movements["y"];
        }

        public async Task<Mission> CreateMission(Agent agent, Target target)
        {
            Mission mission = new Mission();
            mission.Agent = agent;
            mission.Target = target;
            mission.TimeLeft = MissionUtilities.CalculateTime(target.Location, agent.Location);
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return mission;
            
        }

        public async Task DeleteAllMissions(int? targetId, int agentId)
        {
            List<Mission> missions = _context.Missions
                .Where(m => m.TargetId == targetId || m.AgentId == agentId)
                .ToList();
            _context.RemoveRange(missions);
            await _context.SaveChangesAsync();
        }

        public bool IsInDistance(Target target, Agent agent)
        {
            bool isInDistance = false;
            int distance = MissionUtilities.CalculateDistance(target.Location, agent.Location);
            if (distance < 200)
            {
                isInDistance = true;
            }
            return isInDistance;
        }

        public async Task<List<Mission>> IsRelevant(List<Mission> missions)
        {
            foreach (Mission mission in missions)
            {
                if (!IsInDistance(mission.Target, mission.Agent))
                {
                    missions.Remove(mission);
                }
            }
            await _context.SaveChangesAsync();
            return missions;
        }

        public async Task<List<Mission>> IsNew(List<Mission> missions)
        {
            List<Target> targets = _context.Targets
                .Where(t => t.Status == TargetStatus.Alive)
                .ToList();
            List<Agent> agents = _context.Agents
                .Where(t => t.Status == AgentStatus.InActive)
                .Include(t => t.Location)
                .Include(t => t.Status)
                .Include(t => t.Missions)
                .ToList();
            foreach (Target target in targets)
            {
                foreach (Agent agent in agents)
                {
                    if (IsInDistance(target, agent))
                    {
                        Mission mission = await CreateMission(agent, target);
                        missions .Add(mission);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return missions;

        }
    }
}

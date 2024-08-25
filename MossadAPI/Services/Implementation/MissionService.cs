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
                int timeRemaining = MissionUtilities.CalculateTime(mission.Target.Location, mission.Agent.Location);
                mission.TimeLeft = timeRemaining;
                _context.Missions.Update(mission);
                if (MissionUtilities.CalculateDistance(mission.Target.Location, mission.Agent.Location) <= 1)
                {
                    UpdateStatus(mission);
                    UpdateMovement(mission);
                    _context.Missions.Update(mission);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mission>> GetAllMissions()
        {
            List<Mission> missions = _context.Missions.ToList();
            if (missions == null)
            {
                throw new ArgumentNullException(nameof(missions));
            }
            return missions;
        }

        public async Task UpdateMissionStatus(int id, UpdateStatusDTO missionDTO)
        {
            Mission mission = _context.Missions.Find(id);
            if (mission == null)
            {
                throw new ArgumentNullException(nameof(mission));
            }
            if (missionDTO.Status == "assigned")
            {
                mission.Status = MissionStatus.InMission;
                await _context.SaveChangesAsync();
            }
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
            mission.Agent.Location.X = movements["x"];
            mission.Agent.Location.Y = movements["y"];
        }
    }
}

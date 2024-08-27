using Microsoft.EntityFrameworkCore;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Agents;
using MossadAPI.Services.Interfaces;
using MossadAPI.Services.Implementation;
using MossadAPI.Data;
using MossadAPI.Services.Utilities;
using MossadAPI.Services.DTO.Targets;
using MossadAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MossadAPI.Services.Implementation
{
    public class AgentService : IAgentService
    {
        private readonly MossadAPIContext _context;
        private readonly MissionService _missionService;
        
        public AgentService(MossadAPIContext context, MissionService missionService)
        {
            _context = context;
            _missionService = missionService;
        }
        
        public async Task<List<Agent>> GetAllAgents()
        {
            List<Agent> agents;
            agents = await _context.Agents.ToListAsync();
            return agents;
        }

        public async Task MoveAgent(int agentId, AgentMovementDTO movementDTO)
        {
            Agent agent = await _context.Agents.FindAsync(agentId);
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }
            else if (agent.Status == AgentStatus.Active)
            {
                throw new InvalidOperationException(nameof(agent));
            }
            Dictionary<string, int> movement = AgentUtilities.CalculateMovement(movementDTO.direction, agent.Location.X, agent.Location.Y);
            agent.Location.X += movement["x"];
            agent.Location.Y += movement["y"];
            if (agent.Location.X < 1000 && agent.Location.Y < 1000)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("OutOfRange");
            }
            
        }

        public async Task<int> PostAgent(AgentDTO agentDTO)
        {
            if (agentDTO == null)
            {
                throw new ArgumentNullException(nameof(agentDTO));
            }
            Agent agent = new Agent();
            agent.NickName = agentDTO.nickName;
            agent.ImageUrl = agentDTO.photo_url;
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();
            return agent.Id;
        }

        public async Task SetFirstLocation(int agentId, AgentLocationDTO locationDTO)
        {
            Agent agent = await _context.Agents.FindAsync(agentId);
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }
            agent.Location.X = locationDTO.x;
            agent.Location.Y = locationDTO.y;
            await _context.SaveChangesAsync();
            await ModifyOffers(agent);
        }

        public async Task ModifyOffers(Agent agent)
        {
            List<Target> targets = _context.Targets
                .Where(t => t.Status == TargetStatus.Alive && t.Mission == null)
                .Include(t => t.Location)
                .Include(t => t.Status)
                .Include(t => t.Mission)
                .ToList();
            
            foreach (Target target in targets)
            {
                if (_missionService.IsInDistance(target, agent))
                {
                    await _missionService.CreateMission(agent, target);
                }
            }
            await _context.SaveChangesAsync();
        }

        

        

        
    }
}

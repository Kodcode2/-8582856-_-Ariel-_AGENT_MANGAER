using Microsoft.EntityFrameworkCore;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Agents;
using MossadAPI.Services.Interfaces;
using MossadAPI.Data;
using MossadAPI.Services.Utilities;
using MossadAPI.Services.DTO.Targets;

namespace MossadAPI.Services.Implementation
{
    public class AgentService : IAgentService
    {
        private readonly MossadAPIContext _context;
        public AgentService(MossadAPIContext context)
        {
            _context = context;
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
            Dictionary<string, int> movement = AgentUtilities.CalculateMovement(movementDTO.Direction, agent.Location.X, agent.Location.Y);
            agent.Location.X = movement["x"];
            agent.Location.Y = movement["y"];
            await _context.SaveChangesAsync();
        }

        public async Task<int> PostAgent(AgentDTO agentDTO)
        {
            if (agentDTO == null)
            {
                throw new ArgumentNullException(nameof(agentDTO));
            }
            Agent agent = new Agent();
            agent.NickName = agentDTO.NickName;
            agent.ImageUrl = agentDTO.PhotoUrl;
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
            agent.Location.X = locationDTO.X;
            agent.Location.Y = locationDTO.Y;
            await _context.SaveChangesAsync();
        }
    }
}

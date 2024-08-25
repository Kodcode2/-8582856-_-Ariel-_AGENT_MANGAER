using Microsoft.AspNetCore.Mvc;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Agents;

namespace MossadAPI.Services.Interfaces
{
    public interface IAgentService
    {
        Task<int> PostAgent(AgentDTO agentDTO);
        Task<List<Agent>> GetAllAgents();
        Task SetFirstLocation(int agentId, AgentLocationDTO locationDTO);
        Task MoveAgent(int agentId, AgentMovementDTO movementDTO);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Agents;
using MossadAPI.Services.Implementation;

namespace MossadAPI.Controllers
{
    [ApiController]
    [Route("agents")]
    public class AgentsController : ControllerBase
    {
        private readonly AgentService _agentService;
        public AgentsController(AgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        [Route("/agents")]
        public async Task<ActionResult> GetAgents()
        {
            try
            {
                List<Agent> agents = await _agentService.GetAllAgents();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/agents")]
        public async Task<ActionResult> CreateAgent([FromBody] AgentDTO agentDTO)
        {
            try
            {
                int id = await _agentService.PostAgent(agentDTO);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/agents/{id}/pin")]
        public async Task<ActionResult> SetLocation([FromBody] AgentLocationDTO agentDTO, int id)
        {
            try
            {
                await _agentService.SetFirstLocation(id, agentDTO);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/agents/{id}/move")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] AgentMovementDTO agentMovementDTO)
        {
            try
            {
                await _agentService.MoveAgent(id, agentMovementDTO);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

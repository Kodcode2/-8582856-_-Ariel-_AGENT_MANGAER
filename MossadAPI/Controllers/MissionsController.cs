using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Data;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Missions;
using MossadAPI.Services.Implementation;

namespace MossadAPI.Controllers
{
    public class MissionsController : ControllerBase
    {
        private readonly MissionService _missionService;
        public MissionsController(MissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpPut]
        [Route("/missions/update")]
        public async Task<ActionResult> UpdateMission()
        {
            try
            {
                _missionService.ControlMissions();
                return Ok("Great");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/missions")]
        public async Task<ActionResult> GetMissions()
        {
            try
            {
                List<Mission> missions = await _missionService.GetMissions();
                return Ok("Great");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/missions/{id}")]
        public async Task<ActionResult> UpdateStatus(int id, UpdateStatusDTO status)
        {
            try
            {
                _missionService.UpdateMissionStatus(id, status);
                return Ok("Great");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Targets;
using MossadAPI.Services.Interfaces;
using MossadAPI.Services.Utilities;
using MossadAPI.Data;
using MossadAPI.Services.Implementation;
using MossadAPI.Services.DTO.Agents;

namespace MossadAPI.Controllers
{
    public class TargetsController : ControllerBase
    {
        private readonly TargetService _targetService;
        public TargetsController(TargetService targetService)
        {
            _targetService = targetService;
        }

        [HttpPost]
        [Route("/targets")]
        public async Task<ActionResult> CreateTarget(TargetDTO targetDTO)
        {
            try
            {
                int? id = await _targetService.PostTarget(targetDTO);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/targets")]
        public async Task<ActionResult> GetTargets()
        {
            try
            {
                List<Target> targets = await _targetService.GetAllTargets();
                return Ok(targets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/targets/{id}/pin")]
        public async Task<ActionResult> SetLocation(TargetLocationDTO targetDTO, int id)
        {
            try
            {
                _targetService.SetFirstLocation(id, targetDTO);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/targets/{id}/move")]
        public async Task<ActionResult> UpdateLocation(int id, TargetMovementDTO targetMovementDTO)
        {
            try
            {
                _targetService.MoveTarget(id, targetMovementDTO);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

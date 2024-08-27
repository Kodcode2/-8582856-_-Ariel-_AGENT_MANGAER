using MossadAPI.Services.Interfaces;
using MossadAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MossadAPI.Services.DTO.Targets;
using MossadAPI.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Services.Utilities;
using MossadAPI.Enums;


namespace MossadAPI.Services.Implementation
{
    public class TargetService : ITargetService
    {
        private readonly MossadAPIContext _context;
        private readonly MissionService _missionService;
        public TargetService(MossadAPIContext context, MissionService missionService)
        {
            _context = context;
            _missionService = missionService;
        }
        public async Task<List<Target>> GetAllTargets()
        {
            List<Target> targets;
            targets = await _context.Targets.ToListAsync();
            return targets;
        }

        public async Task MoveTarget(int targetId, TargetMovementDTO movementDTO)
        {
            Target target = await _context.Targets.FindAsync(targetId);
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            Dictionary<string, int> movement = TargetUtilities.CalculateMovement(movementDTO.direction, target.Location.X, target.Location.Y);
            target.Location.X = movement["x"];
            target.Location.Y = movement["y"];
            if (target.Location.X < 1000 && target.Location.Y < 1000)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("OutOfRange");
            }
        }

        public async Task<int?> PostTarget(TargetDTO targetDTO)
        {
           if (targetDTO == null)
            {
                throw new ArgumentNullException(nameof(targetDTO));
            }
           Target target = new Target();
            target.Name = targetDTO.name;
            target.Position = targetDTO.position;
            target.ImageUrl = targetDTO.photo_url;
           _context.Targets.Add(target);
           await _context.SaveChangesAsync();
           return target.Id;
        }

        public async Task SetFirstLocation(int targetId, TargetLocationDTO locationDTO)
        {
            Target target = await _context.Targets.FindAsync(targetId);
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            target.Location.X = locationDTO.x;
            target.Location.Y = locationDTO.y;
            await _context.SaveChangesAsync();
            await ModifyOffers(target);
        }

        public async Task ModifyOffers(Target target)
        {
            List<Agent> agents = _context.Agents
                .Where(t => t.Status == AgentStatus.InActive)
                .Include(t => t.Location)
                .Include(t => t.Status)
                .Include(t => t.Missions)
                .ToList();

            foreach (Agent agent in agents)
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

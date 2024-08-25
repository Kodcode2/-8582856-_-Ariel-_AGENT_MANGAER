using MossadAPI.Services.Interfaces;
using MossadAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MossadAPI.Services.DTO.Targets;
using MossadAPI.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Services.Utilities;


namespace MossadAPI.Services.Implementation
{
    public class TargetService : ITargetService
    {
        private readonly MossadAPIContext _context;
        public TargetService(MossadAPIContext context)
        {
            _context = context;
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
            Dictionary<string, int> movement = TargetUtilities.CalculateMovement(movementDTO.Direction, target.Location.X, target.Location.Y);
            target.Location.X = movement["x"];
            target.Location.Y = movement["y"];
            await _context.SaveChangesAsync();
        }

        public async Task<int?> PostTarget(TargetDTO targetDTO)
        {
           if (targetDTO == null)
            {
                throw new ArgumentNullException(nameof(targetDTO));
            }
           Target target = new Target();
            target.Name = targetDTO.Name;
            target.Position = targetDTO.Position;
            target.ImageUrl = targetDTO.PhotoUrl;
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
            target.Location.X = locationDTO.X;
            target.Location.Y = locationDTO.Y;
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Targets;

namespace MossadAPI.Services.Interfaces
{
    public interface ITargetService
    {
        Task<int?> PostTarget(TargetDTO targetDTO);
        Task<List<Target>> GetAllTargets();
        Task SetFirstLocation(int targetId, TargetLocationDTO locationDTO);
        Task MoveTarget(int targetId, TargetMovementDTO movementDTO);
    }
}

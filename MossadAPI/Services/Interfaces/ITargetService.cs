using Microsoft.AspNetCore.Mvc;
using MossadAPI.Models;
using MossadAPI.Services.DTO;

namespace MossadAPI.Services.Interfaces
{
    public interface ITargetService
    {
        Task<ActionResult<int>> PostTarget(TargetDTO targetDTO);
        Task<ActionResult<List<Target>>> GetAllTargets();
    }
}

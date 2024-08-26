using Microsoft.AspNetCore.Mvc;
using MossadAPI.Models;
using MossadAPI.Services.DTO.Missions;

namespace MossadAPI.Services.Interfaces
{
    public interface IMissionService
    {
        Task ControlMissions();
        Task<List<Mission>> GetMissions();
        Task UpdateMissionStatus(int id, UpdateStatusDTO missionDTO);
    }
}

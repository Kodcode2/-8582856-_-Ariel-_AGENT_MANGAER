using MossadAPI.Models;
using MossadAPI.Enums;

namespace MossadAPI.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string ImageUrl { get; set; }
        public int? LocationId { get; set; }
        public AgentLocation? Location { get; set; }
        public AgentStatus Status { get; set; } = AgentStatus.InActive;
        public List<Mission>? Missions { get; set; }
    }
}

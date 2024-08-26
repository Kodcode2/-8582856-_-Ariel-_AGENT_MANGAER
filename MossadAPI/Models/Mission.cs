using MossadAPI.Enums;
using MossadAPI.Models;

namespace MossadAPI.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int TargetId { get; set; }
        public Target Target { get; set; }
        public int TimeLeft { get; set; }
        public int? TotalTime { get; set; }
        public MissionStatus Status { get; set; } = MissionStatus.OnOffer;
    }
}

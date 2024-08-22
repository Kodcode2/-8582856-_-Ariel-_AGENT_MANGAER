using MossadAPI.Enums;
using MossadAPI.Models;

namespace MossadAPI.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position {  get; set; }
        public int? LocationId { get; set; }
        public TargetLocation? Location { get; set; }
        public TargetStatus Status { get; set; } = TargetStatus.Alive;
        public int? MissionId { get; set; }
        public Mission? Mission { get; set; }
    }
}

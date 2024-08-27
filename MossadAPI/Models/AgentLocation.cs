using System.ComponentModel.DataAnnotations.Schema;

namespace MossadAPI.Models
{
    public class AgentLocation
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        [NotMapped]
        public Agent Agent { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

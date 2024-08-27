using System.ComponentModel.DataAnnotations.Schema;

namespace MossadAPI.Models
{
    public class TargetLocation
    {
        public int Id { get; set; }
        
        public int TargetId { get; set; }
        [NotMapped]
        public Target Target { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

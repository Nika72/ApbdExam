using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Models
{
    public class AstronautMission
    {
        public int AstronautId { get; set; }
        public Astronaut Astronaut { get; set; }

        public int MissionId { get; set; }
        public Mission Mission { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; }
    }
}
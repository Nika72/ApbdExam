using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Dtos
{
    public class AssignAstronautDto
    {
        [Required]
        public int AstronautId { get; set; }

        [Required]
        public int MissionId { get; set; }
    }
}
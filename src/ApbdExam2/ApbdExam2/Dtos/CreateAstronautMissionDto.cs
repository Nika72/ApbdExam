using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Dtos
{
    public class CreateAstronautMissionDto
    {
        [Required]
        public int AstronautId { get; set; }

        [Required]
        [StringLength(100)]
        public string Role { get; set; }
    }
}

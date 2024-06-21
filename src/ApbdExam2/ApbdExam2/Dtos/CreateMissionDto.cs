
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Dtos
{
    public class CreateMissionDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? LaunchDate { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public IEnumerable<CreateAstronautMissionDto> AstronautMissions { get; set; }
    }
}

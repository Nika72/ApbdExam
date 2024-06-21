using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Models
{
    public class Mission
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? LaunchDate { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<AstronautMission> AstronautMissions { get; set; }
    }
}

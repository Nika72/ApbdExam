using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Models
{
    public class Astronaut
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Nationality { get; set; }

        public ICollection<AstronautMission> AstronautMissions { get; set; }
    }
}

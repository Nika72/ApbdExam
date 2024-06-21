using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApbdExam2.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}

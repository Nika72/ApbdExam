using System;
using System.Collections.Generic;

namespace ApbdExam2.Dtos
{
    public class MissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? LaunchDate { get; set; }
        public string OrganizationName { get; set; }
        public IEnumerable<AstronautMissionDto> AstronautMissions { get; set; }
    }
}

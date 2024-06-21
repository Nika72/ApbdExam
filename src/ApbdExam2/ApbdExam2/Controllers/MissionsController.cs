using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Dtos;
using ApbdExam2.Models;
using ApbdExam2.Services;

namespace ApbdExam2.Controllers
{
    [Route("api/missions")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions([FromQuery] string sortBy, CancellationToken cancellationToken = default)
        {
            var missions = await _missionService.GetMissionsAsync(sortBy, cancellationToken);
            var missionDtos = missions.Select(m => new MissionDto
            {
                Id = m.Id,
                Name = m.Name,
                LaunchDate = m.LaunchDate,
                OrganizationName = m.Organization.Name,
                AstronautMissions = m.AstronautMissions.Select(am => new AstronautMissionDto
                {
                    AstronautName = am.Astronaut.FullName,
                    Role = am.Role
                }).ToList()
            }).ToList();

            return Ok(missionDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MissionDto>> GetMission(int id, CancellationToken cancellationToken = default)
        {
            var mission = await _missionService.GetMissionByIdAsync(id, cancellationToken);

            if (mission == null)
            {
                return NotFound($"Mission with ID {id} not found!");
            }

            var missionDto = new MissionDto
            {
                Id = mission.Id,
                Name = mission.Name,
                LaunchDate = mission.LaunchDate,
                OrganizationName = mission.Organization.Name,
                AstronautMissions = mission.AstronautMissions.Select(am => new AstronautMissionDto
                {
                    AstronautName = am.Astronaut.FullName,
                    Role = am.Role
                }).ToList()
            };

            return Ok(missionDto);
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> CreateMission([FromBody] CreateMissionDto createMissionDto, CancellationToken cancellationToken = default)
        {
            if (createMissionDto == null)
            {
                return BadRequest("Invalid mission data.");
            }

            var mission = new Mission
            {
                Name = createMissionDto.Name,
                LaunchDate = createMissionDto.LaunchDate,
                OrganizationId = createMissionDto.OrganizationId,
                AstronautMissions = createMissionDto.AstronautMissions.Select(am => new AstronautMission
                {
                    AstronautId = am.AstronautId,
                    Role = am.Role
                }).ToList()
            };

            await _missionService.CreateMissionAsync(mission, cancellationToken);

            return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, mission);
        }

        [HttpPost("astronaut")]
        public async Task<IActionResult> AssignAstronautToMission([FromBody] AssignAstronautDto assignAstronautDto, CancellationToken cancellationToken = default)
        {
            if (assignAstronautDto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _missionService.AssignAstronautToMissionAsync(assignAstronautDto.AstronautId, assignAstronautDto.MissionId, cancellationToken);

            return Ok();
        }
    }
}

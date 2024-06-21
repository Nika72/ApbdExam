using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Dtos;
using ApbdExam2.Models;
using ApbdExam2.Services;

namespace ApbdExam2.Controllers
{
    [Route("api/missions")]
    [Authorize]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions(CancellationToken cancellationToken = default)
        {
            try
            {
                var missions = await _missionService.GetMissionsAsync(cancellationToken);
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MissionDto>> GetMission(int id, CancellationToken cancellationToken = default)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
            }
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMission(int id, CancellationToken cancellationToken = default)
        {
            var mission = await _missionService.GetMissionByIdAsync(id, cancellationToken);
            if (mission == null)
            {
                return NotFound($"Mission with ID {id} not found!");
            }

            await _missionService.DeleteMissionAsync(id, cancellationToken);
            return NoContent();
        }
    }
}

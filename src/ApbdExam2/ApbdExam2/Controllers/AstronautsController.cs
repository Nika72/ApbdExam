using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Dtos;
using ApbdExam2.Services;

namespace ApbdExam2.Controllers
{
    [Route("api/astronauts")]
    [ApiController]
    public class AstronautsController : ControllerBase
    {
        private readonly IAstronautService _astronautService;

        public AstronautsController(IAstronautService astronautService)
        {
            _astronautService = astronautService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AstronautDto>>> GetAstronauts([FromQuery] string sortBy, CancellationToken cancellationToken = default)
        {
            var astronauts = await _astronautService.GetAstronautsAsync(sortBy, cancellationToken);
            var astronautDtos = astronauts.Select(a => new AstronautDto
            {
                Id = a.Id,
                FullName = a.FullName,
                BirthDate = a.BirthDate,
                Nationality = a.Nationality
            }).ToList();

            return Ok(astronautDtos);
        }
    }
}
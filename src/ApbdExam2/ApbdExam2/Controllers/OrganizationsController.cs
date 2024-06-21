using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Dtos;
using ApbdExam2.Services;

namespace ApbdExam2.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetOrganizations(CancellationToken cancellationToken = default)
        {
            var organizations = await _organizationService.GetOrganizationsAsync(cancellationToken);
            var organizationDtos = organizations.Select(o => new OrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                Country = o.Country
            }).ToList();

            return Ok(organizationDtos);
        }
    }
}
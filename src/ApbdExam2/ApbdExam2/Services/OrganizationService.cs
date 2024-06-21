using Microsoft.EntityFrameworkCore;
using ApbdExam2.Data;
using ApbdExam2.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApbdExam2.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly SpaceExplorerContext _context;

        public OrganizationService(SpaceExplorerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(CancellationToken cancellationToken)
        {
            return await _context.Organizations.OrderBy(o => o.Name).ToListAsync(cancellationToken);
        }
    }
}
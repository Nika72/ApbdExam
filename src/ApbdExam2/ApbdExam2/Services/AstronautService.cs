using Microsoft.EntityFrameworkCore;
using ApbdExam2.Data;
using ApbdExam2.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApbdExam2.Services
{
    public class AstronautService : IAstronautService
    {
        private readonly SpaceExplorerContext _context;

        public AstronautService(SpaceExplorerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Astronaut>> GetAstronautsAsync(string sortBy, CancellationToken cancellationToken)
        {
            var query = _context.Astronauts.AsQueryable();

            switch (sortBy?.ToLower())
            {
                case "name":
                    query = query.OrderBy(a => a.FullName);
                    break;
                case "birthdate":
                    query = query.OrderBy(a => a.BirthDate);
                    break;
                case "nationality":
                    query = query.OrderBy(a => a.Nationality);
                    break;
                default:
                    query = query.OrderBy(a => a.FullName);
                    break;
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
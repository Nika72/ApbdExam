using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Models;

namespace ApbdExam2.Services
{
    public interface IAstronautService
    {
        Task<IEnumerable<Astronaut>> GetAstronautsAsync(string sortBy, CancellationToken cancellationToken);
    }
}
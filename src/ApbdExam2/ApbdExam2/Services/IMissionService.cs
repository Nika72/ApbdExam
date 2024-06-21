using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Models;

namespace ApbdExam2.Services
{
    public interface IMissionService
    {
        Task<IEnumerable<Mission>> GetMissionsAsync(CancellationToken cancellationToken);
        Task<Mission> GetMissionByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateMissionAsync(Mission mission, CancellationToken cancellationToken);
        Task DeleteMissionAsync(int id, CancellationToken cancellationToken);
    }
}
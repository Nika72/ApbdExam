using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApbdExam2.Models;

namespace ApbdExam2.Services
{
    public interface IMissionService
    {
        Task<IEnumerable<Mission>> GetMissionsAsync(string sortBy, CancellationToken cancellationToken);
        Task<Mission> GetMissionByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateMissionAsync(Mission mission, CancellationToken cancellationToken);
        Task AssignAstronautToMissionAsync(int astronautId, int missionId, CancellationToken cancellationToken);
    }
}
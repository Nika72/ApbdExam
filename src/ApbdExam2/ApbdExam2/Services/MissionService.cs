using Microsoft.EntityFrameworkCore;
using ApbdExam2.Data;
using ApbdExam2.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApbdExam2.Services
{
    public class MissionService : IMissionService
    {
        private readonly SpaceExplorerContext _context;

        public MissionService(SpaceExplorerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mission>> GetMissionsAsync(string sortBy, CancellationToken cancellationToken)
        {
            var query = _context.Missions.Include(m => m.Organization).Include(m => m.AstronautMissions).ThenInclude(am => am.Astronaut).AsQueryable();

            switch (sortBy?.ToLower())
            {
                case "date":
                    query = query.OrderBy(m => m.LaunchDate);
                    break;
                default:
                    query = query.OrderBy(m => m.Name);
                    break;
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Mission> GetMissionByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Missions.Include(m => m.Organization).Include(m => m.AstronautMissions).ThenInclude(am => am.Astronaut).FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task CreateMissionAsync(Mission mission, CancellationToken cancellationToken)
        {
            await _context.Missions.AddAsync(mission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AssignAstronautToMissionAsync(int astronautId, int missionId, CancellationToken cancellationToken)
        {
            var astronautMission = new AstronautMission
            {
                AstronautId = astronautId,
                MissionId = missionId,
                Role = "Assigned" // You can adjust the role as needed
            };

            await _context.AstronautMissions.AddAsync(astronautMission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

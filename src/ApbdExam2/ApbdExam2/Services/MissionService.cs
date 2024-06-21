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

        public async Task<IEnumerable<Mission>> GetMissionsAsync(CancellationToken cancellationToken)
        {
            return await _context.Missions.Include(m => m.Organization).Include(m => m.AstronautMissions).ThenInclude(am => am.Astronaut).ToListAsync(cancellationToken);
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

        public async Task DeleteMissionAsync(int id, CancellationToken cancellationToken)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission != null)
            {
                _context.Missions.Remove(mission);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
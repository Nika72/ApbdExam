using ApbdExam2.Models;
using Microsoft.EntityFrameworkCore;
using space_explorer.Models;

namespace ApbdExam2.Data
{
    public class SpaceExplorerContext : DbContext
    {
        public SpaceExplorerContext(DbContextOptions<SpaceExplorerContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Astronaut> Astronauts { get; set; }
        public DbSet<AstronautMission> AstronautMissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AstronautMission>()
                .HasKey(am => new { am.AstronautId, am.MissionId });

            modelBuilder.Entity<AstronautMission>()
                .HasOne(am => am.Astronaut)
                .WithMany(a => a.AstronautMissions)
                .HasForeignKey(am => am.AstronautId);

            modelBuilder.Entity<AstronautMission>()
                .HasOne(am => am.Mission)
                .WithMany(m => m.AstronautMissions)
                .HasForeignKey(am => am.MissionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

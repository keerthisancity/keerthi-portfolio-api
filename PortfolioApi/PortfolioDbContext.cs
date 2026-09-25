using PortfolioApi.DbModels;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApi
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        public DbSet<Bio> Bio { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for ProjectSkills
            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectID, ps.SkillID });

            // Relationships
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Bio)
                .WithMany(b => b.Projects)
                .HasForeignKey(p => p.BioID);

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Bio)
                .WithMany(b => b.Skills)
                .HasForeignKey(s => s.BioID);

            modelBuilder.Entity<Experience>()
                .HasOne(e => e.Bio)
                .WithMany(b => b.Experience)
                .HasForeignKey(e => e.BioID);

            modelBuilder.Entity<Education>()
                .HasOne(ed => ed.Bio)
                .WithMany(b => b.Education)
                .HasForeignKey(ed => ed.BioID);

            modelBuilder.Entity<Achievement>()
                .HasOne(a => a.Bio)
                .WithMany(b => b.Achievements)
                .HasForeignKey(a => a.BioID);
        }
    }

}

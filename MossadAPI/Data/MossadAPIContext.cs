using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MossadAPI.Models;

namespace MossadAPI.Data
{
    public class MossadAPIContext : DbContext
    {
        public MossadAPIContext (DbContextOptions<MossadAPIContext> options)
            : base(options)
        {
        }

        public DbSet<MossadAPI.Models.Agent> Agents { get; set; } = default!;
        public DbSet<MossadAPI.Models.Target> Targets { get; set; } = default!;
        public DbSet<MossadAPI.Models.Mission> Missions { get; set; } = default!;
        public DbSet<MossadAPI.Models.AgentLocation> AgentLocations { get; set; } = default!;
        public DbSet<MossadAPI.Models.TargetLocation> TargetLocations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Target)
                .WithOne(t => t.Mission)
                .HasForeignKey<Target>(t => t.MissionId);

            modelBuilder.Entity<Agent>()
                .HasOne(a => a.Location)
                .WithOne(l => l.Agent)
                .HasForeignKey<AgentLocation>(l => l.AgentId);
            
            modelBuilder.Entity<Target>()
                .HasOne(t => t.Location)
                .WithOne(l => l.Target)
                .HasForeignKey<TargetLocation>(l => l.TargetId);
        }
    }
}

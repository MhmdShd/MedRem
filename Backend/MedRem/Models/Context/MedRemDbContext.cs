using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MedRem.Models.Context
{
    public class MedRemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pill> Pills { get; set; }
        public DbSet<PillSchedule> PillSchedules { get; set; }
        public DbSet<Report> Reports { get; set; }

        public MedRemDbContext(DbContextOptions<MedRemDbContext> options)
            : base(options)
        {
        }

        // Fluent API to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
    .HasMany(x => x.PillSchedules)
    .WithOne(x => x.User)
    .HasForeignKey(x => x.UserId)
    .OnDelete(DeleteBehavior.Cascade); // assuming you want cascading on PillSchedules

            modelBuilder.Entity<User>()
                .HasMany(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // assuming you want cascading on Reports

            modelBuilder.Entity<User>()
                .HasMany(x => x.Pills)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); // this will prevent cascading delete on Pills

            modelBuilder.Entity<Pill>()
                .HasMany(x => x.PillSchedules)
                .WithOne(x => x.Pill)
                .HasForeignKey(x => x.PillId)
                .OnDelete(DeleteBehavior.Cascade); // assuming 
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

using RitAutomationTestTask1.Domain.Models.EntityFrameworkModels;

namespace RitAutomationTestTask1.Domain.Services.MapDbContext
{
    public partial class MapDbModel : DbContext
    {
        public MapDbModel()
            : base("name=MapDbModel")
        {
        }

        public virtual DbSet<MachinePosition> MachinePositions { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>()
                .HasOptional(e => e.MachinePosition)
                .WithRequired(e => e.Machine)
                .WillCascadeOnDelete();
        }
    }
}

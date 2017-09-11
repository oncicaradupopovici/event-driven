using Microsoft.EntityFrameworkCore;
using Charisma.Contracts.ReadModel.Entities;
using Charisma.SharedKernel.Data.Abstractions;

namespace Charisma.Contracts.Data
{
    public class CharismaContractsDbContext : DbContext
    {
        //public DbSet<Contract> Contracts { get; set; }
        public DbSet<EventDescriptor> EventDescriptors { get; set; }

        public CharismaContractsDbContext() { }

        public CharismaContractsDbContext(DbContextOptions<CharismaContractsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContractReadModel>()
                .ToTable("Contracts")
                .HasKey(c => c.Id);

            modelBuilder.Entity<ContractReadModel>()
                .HasMany(c => c.ContractLines)
                .WithOne()
                .HasForeignKey(cl => cl.ContractId);

            modelBuilder.Entity<ContractLineReadModel>()
                .ToTable("ContractLines")
                .HasKey(c => c.Id);







            modelBuilder.Entity<EventDescriptor>()
                .ToTable("EventStore")
                .HasKey(ed => ed.EventId);
            modelBuilder.Entity<EventDescriptor>()
                .Property(p => p.EventData)
                .HasColumnName("EventData")
                .IsRequired();

            modelBuilder.Entity<EventDescriptor>()
                .Ignore(p => p.EventData);
        }
    }
}

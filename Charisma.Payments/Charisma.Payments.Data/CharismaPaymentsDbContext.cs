using Microsoft.EntityFrameworkCore;
using Charisma.SharedKernel.Core;

namespace Charisma.Payments.Data
{
    public class CharismaPaymentsDbContext : DbContext
    {
        //public DbSet<Contract> Contracts { get; set; }
        public DbSet<EventDescriptor> EventDescriptors { get; set; }

        public CharismaPaymentsDbContext() { }

        public CharismaPaymentsDbContext(DbContextOptions<CharismaPaymentsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

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

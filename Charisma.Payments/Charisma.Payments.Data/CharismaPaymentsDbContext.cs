using Charisma.Payments.Domain.PayableAggregate;
using Microsoft.EntityFrameworkCore;
using Charisma.SharedKernel.Data.Abstractions;

namespace Charisma.Payments.Data
{
    public class CharismaPaymentsDbContext : DbContext
    {
        public DbSet<Payable> Payables { get; set; }
        public DbSet<EventDescriptor> EventDescriptors { get; set; }

        public CharismaPaymentsDbContext() { }

        public CharismaPaymentsDbContext(DbContextOptions<CharismaPaymentsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payable>()
                .ToTable("Payables")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Payment>()
                .ToTable("Payments")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Payable>()
                .HasOne(payable => payable.Payment)
                .WithOne()
                .HasForeignKey<Payment>(payment => payment.PayableId);

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

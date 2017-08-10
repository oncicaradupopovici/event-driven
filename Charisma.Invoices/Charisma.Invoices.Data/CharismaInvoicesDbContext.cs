using Charisma.Invoices.Domain.InvoiceAggregate;
using Charisma.SharedKernel.Core;
using Charisma.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Invoices.Data
{
    public class CharismaInvoicesDbContext : DbContext
    {
        //public DbSet<Contract> Contracts { get; set; }
        public DbSet<EventDescriptor> EventDescriptors { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public CharismaInvoicesDbContext() { }

        public CharismaInvoicesDbContext(DbContextOptions<CharismaInvoicesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>()
                .ToTable("Invoices")
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

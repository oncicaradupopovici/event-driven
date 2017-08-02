using System;
using System.Collections.Generic;
using System.Text;
using Charisma.Contracts.Domain.Aggregates;
using Charisma.Contracts.Domain.ReadModel;
using Microsoft.EntityFrameworkCore;
using Charisma.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

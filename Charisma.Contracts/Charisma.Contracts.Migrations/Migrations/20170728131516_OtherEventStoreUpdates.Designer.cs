using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Charisma.Contracts.Data;

namespace Charisma.Contracts.Migrations.Migrations
{
    [DbContext(typeof(CharismaContractsDbContext))]
    [Migration("20170728131516_OtherEventStoreUpdates")]
    partial class OtherEventStoreUpdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Charisma.Contracts.Domain.Aggregates.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("ClientId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Charisma.Shared.Domain.EventDescriptor", b =>
                {
                    b.Property<int>("EvId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AggregateId");

                    b.Property<string>("EventType");

                    b.Property<string>("JsonEventData");

                    b.Property<int>("Version");

                    b.HasKey("EvId");

                    b.ToTable("EventStore");
                });
        }
    }
}

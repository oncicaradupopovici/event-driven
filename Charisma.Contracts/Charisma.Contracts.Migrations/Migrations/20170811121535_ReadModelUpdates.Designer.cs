using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Charisma.Contracts.Data;

namespace Charisma.Contracts.Migrations.Migrations
{
    [DbContext(typeof(CharismaContractsDbContext))]
    [Migration("20170811121535_ReadModelUpdates")]
    partial class ReadModelUpdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Charisma.Contracts.ReadModel.Entities.ContractLineReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContractId");

                    b.Property<decimal>("Price");

                    b.Property<string>("Product");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("ContractLines");
                });

            modelBuilder.Entity("Charisma.Contracts.ReadModel.Entities.ContractReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("ClientId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Charisma.SharedKernel.Core.EventDescriptor", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AggregateId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("EventType");

                    b.Property<string>("JsonEventData");

                    b.Property<int>("Version");

                    b.HasKey("EventId");

                    b.ToTable("EventStore");
                });

            modelBuilder.Entity("Charisma.Contracts.ReadModel.Entities.ContractLineReadModel", b =>
                {
                    b.HasOne("Charisma.Contracts.ReadModel.Entities.ContractReadModel")
                        .WithMany("ContractLines")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

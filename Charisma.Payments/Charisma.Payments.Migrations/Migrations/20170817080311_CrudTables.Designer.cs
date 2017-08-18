using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Charisma.Payments.Data;

namespace Charisma.Payments.Migrations.Migrations
{
    [DbContext(typeof(CharismaPaymentsDbContext))]
    [Migration("20170817080311_CrudTables")]
    partial class CrudTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Charisma.Payments.Domain.PayableAggregate.Payable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("ClientId");

                    b.Property<Guid?>("InvoiceId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Payables");
                });

            modelBuilder.Entity("Charisma.Payments.Domain.PayableAggregate.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PayableId");

                    b.Property<DateTime>("PaymentDate");

                    b.HasKey("Id");

                    b.HasIndex("PayableId")
                        .IsUnique();

                    b.ToTable("Payments");
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

            modelBuilder.Entity("Charisma.Payments.Domain.PayableAggregate.Payment", b =>
                {
                    b.HasOne("Charisma.Payments.Domain.PayableAggregate.Payable")
                        .WithOne("Payment")
                        .HasForeignKey("Charisma.Payments.Domain.PayableAggregate.Payment", "PayableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Charisma.Invoices.Data;

namespace Charisma.Invoices.Migrations.Migrations
{
    [DbContext(typeof(CharismaInvoicesDbContext))]
    [Migration("20170816115148_UpdateInvoice")]
    partial class UpdateInvoice
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Charisma.Invoices.Domain.InvoiceAggregate.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("ClientId");

                    b.Property<Guid?>("ContractId");

                    b.Property<Guid?>("PaymentId");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
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
        }
    }
}

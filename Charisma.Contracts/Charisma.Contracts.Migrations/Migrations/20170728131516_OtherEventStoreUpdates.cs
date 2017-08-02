using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Charisma.Contracts.Migrations.Migrations
{
    public partial class OtherEventStoreUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventStore",
                table: "EventStore");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "EventStore");

            migrationBuilder.AddColumn<int>(
                name: "EvId",
                table: "EventStore",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventStore",
                table: "EventStore",
                column: "EvId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventStore",
                table: "EventStore");

            migrationBuilder.DropColumn(
                name: "EvId",
                table: "EventStore");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "EventStore",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventStore",
                table: "EventStore",
                column: "EventId");
        }
    }
}

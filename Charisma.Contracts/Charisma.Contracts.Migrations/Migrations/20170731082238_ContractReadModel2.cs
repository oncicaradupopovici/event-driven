using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Charisma.Contracts.Migrations.Migrations
{
    public partial class ContractReadModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "ContractsReadModel");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ContractsReadModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "ContractsReadModel",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ContractsReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractsReadModel",
                table: "ContractsReadModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractsReadModel",
                table: "ContractsReadModel");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ContractsReadModel");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ContractsReadModel");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ContractsReadModel");

            migrationBuilder.RenameTable(
                name: "ContractsReadModel",
                newName: "Contracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");
        }
    }
}

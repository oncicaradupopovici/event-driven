using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Charisma.Contracts.Migrations.Migrations
{
    public partial class ContractReadModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractsReadModel",
                table: "ContractsReadModel");

            migrationBuilder.RenameTable(
                name: "ContractsReadModel",
                newName: "Contracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "ContractsReadModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractsReadModel",
                table: "ContractsReadModel",
                column: "Id");
        }
    }
}

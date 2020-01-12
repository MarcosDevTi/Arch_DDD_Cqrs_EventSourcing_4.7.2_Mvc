using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arch.Infra.Data.Migrations
{
    public partial class InitEventSourcing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    When = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    AggregateId = table.Column<Guid>(nullable: false),
                    Who = table.Column<string>(nullable: true),
                    Assembly = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoredEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    AggregateId = table.Column<Guid>(nullable: false),
                    Who = table.Column<string>(nullable: true),
                    When = table.Column<string>(nullable: true),
                    Assembly = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvent", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEntities");

            migrationBuilder.DropTable(
                name: "StoredEvent");
        }
    }
}

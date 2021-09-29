using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GamemodeDatabase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(61)", maxLength: 61, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PositionX = table.Column<float>(type: "float", nullable: false, defaultValue: 1685.8075f),
                    PositionY = table.Column<float>(type: "float", nullable: false, defaultValue: -2239.2583f),
                    PositionZ = table.Column<float>(type: "float", nullable: false, defaultValue: 13.5469f),
                    FacingAngle = table.Column<float>(type: "float", nullable: false, defaultValue: 179.4454f),
                    JoinDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2021, 9, 29, 17, 44, 23, 809, DateTimeKind.Local).AddTicks(9460)),
                    LastActive = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}

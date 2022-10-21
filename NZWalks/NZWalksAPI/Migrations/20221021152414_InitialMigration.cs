using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalksAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Long = table.Column<double>(type: "float", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_WalkDifficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_WalkDifficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lenght = table.Column<double>(type: "float", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalkDifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Walks_Tb_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Tb_Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_Walks_Tb_WalkDifficulties_WalkDifficultyId",
                        column: x => x.WalkDifficultyId,
                        principalTable: "Tb_WalkDifficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Walks_RegionId",
                table: "Tb_Walks",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Walks_WalkDifficultyId",
                table: "Tb_Walks",
                column: "WalkDifficultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_Walks");

            migrationBuilder.DropTable(
                name: "Tb_Region");

            migrationBuilder.DropTable(
                name: "Tb_WalkDifficulties");
        }
    }
}

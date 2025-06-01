using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VulnData.DataCollector.Domain.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BduData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendor_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendor_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendor_Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendor_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    System = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VulnClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cvss2 = table.Column<double>(type: "float", nullable: false),
                    Cvss3 = table.Column<double>(type: "float", nullable: false),
                    Fixes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasExploit = table.Column<bool>(type: "bit", nullable: false),
                    HasEliminated = table.Column<bool>(type: "bit", nullable: false),
                    Links = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasIncidents = table.Column<bool>(type: "bit", nullable: false),
                    WayToDestroy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WayToFix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cwe_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cwe_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BduData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BduData");
        }
    }
}

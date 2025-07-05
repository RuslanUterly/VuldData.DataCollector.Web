using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VulnData.DataCollector.Domain.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addnvd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NvdData",
                columns: table => new
                {
                    Cve = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cwe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cvss2 = table.Column<double>(type: "float", nullable: false),
                    Cvss3 = table.Column<double>(type: "float", nullable: false),
                    Cvss4 = table.Column<double>(type: "float", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NvdData", x => x.Cve);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NvdData");
        }
    }
}

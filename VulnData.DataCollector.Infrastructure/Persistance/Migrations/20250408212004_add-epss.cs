using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VulnData.DataCollector.Domain.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addepss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EpssData",
                columns: table => new
                {
                    Cve = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Epss = table.Column<double>(type: "float", nullable: true),
                    Percentile = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpssData", x => x.Cve);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpssData");
        }
    }
}

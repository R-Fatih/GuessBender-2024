using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessBender.Server.Migrations
{
    /// <inheritdoc />
    public partial class add_mgr_country : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Confederation",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confederation",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Country");
        }
    }
}

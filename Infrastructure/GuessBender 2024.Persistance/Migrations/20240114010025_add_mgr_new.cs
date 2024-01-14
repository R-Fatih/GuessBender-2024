using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessBender.Server.Migrations
{
    /// <inheritdoc />
    public partial class add_mgr_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Team",
                newName: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CountryId",
                table: "Team",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Country_CountryId",
                table: "Team",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Country_CountryId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_CountryId",
                table: "Team");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Team",
                newName: "CountryCode");
        }
    }
}

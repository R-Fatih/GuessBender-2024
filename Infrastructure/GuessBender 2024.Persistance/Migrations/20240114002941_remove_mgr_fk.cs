using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessBender.Server.Migrations
{
    /// <inheritdoc />
    public partial class remove_mgr_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_League_Country_CountryId",
                table: "League");

            migrationBuilder.DropIndex(
                name: "IX_League_CountryId",
                table: "League");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_League_CountryId",
                table: "League",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_League_Country_CountryId",
                table: "League",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

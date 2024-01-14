using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessBender.Server.Migrations
{
    /// <inheritdoc />
    public partial class match_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

          

            migrationBuilder.AddForeignKey(
                name: "FK_Prediction_AspNetUsers_UserId",
                table: "Prediction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropIndex(
                name: "IX_Prediction_UserId",
                table: "Prediction");

           
        }
    }
}

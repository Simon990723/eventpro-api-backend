using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventProRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOwnershipToRegistrants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Registrants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Registrants_UserId",
                table: "Registrants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrants_AspNetUsers_UserId",
                table: "Registrants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrants_AspNetUsers_UserId",
                table: "Registrants");

            migrationBuilder.DropIndex(
                name: "IX_Registrants_UserId",
                table: "Registrants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Registrants");
        }
    }
}

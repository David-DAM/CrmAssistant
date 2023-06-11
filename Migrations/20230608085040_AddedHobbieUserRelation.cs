using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmAssistant.Migrations
{
    /// <inheritdoc />
    public partial class AddedHobbieUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HobbieUser_Hobbie_HobbiesId",
                table: "HobbieUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobbie",
                table: "Hobbie");

            migrationBuilder.RenameTable(
                name: "Hobbie",
                newName: "Hobbies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HobbieUser_Hobbies_HobbiesId",
                table: "HobbieUser",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HobbieUser_Hobbies_HobbiesId",
                table: "HobbieUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies");

            migrationBuilder.RenameTable(
                name: "Hobbies",
                newName: "Hobbie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobbie",
                table: "Hobbie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HobbieUser_Hobbie_HobbiesId",
                table: "HobbieUser",
                column: "HobbiesId",
                principalTable: "Hobbie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

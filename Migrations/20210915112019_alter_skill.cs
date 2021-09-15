using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class alter_skill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_InternUserId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "InternUserId",
                table: "Skills",
                newName: "InternStudentInternProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_InternUserId",
                table: "Skills",
                newName: "IX_Skills_InternStudentInternProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_StudentInternProfiles_InternStudentInternProfileId",
                table: "Skills",
                column: "InternStudentInternProfileId",
                principalTable: "StudentInternProfiles",
                principalColumn: "StudentInternProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_StudentInternProfiles_InternStudentInternProfileId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "InternStudentInternProfileId",
                table: "Skills",
                newName: "InternUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_InternStudentInternProfileId",
                table: "Skills",
                newName: "IX_Skills_InternUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_InternUserId",
                table: "Skills",
                column: "InternUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

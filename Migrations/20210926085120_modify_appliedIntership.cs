using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class modify_appliedIntership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Internships");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "AppliedInternships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "AppliedInternships");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Internships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

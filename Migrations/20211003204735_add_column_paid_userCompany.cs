using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class add_column_paid_userCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaidAccount",
                table: "UserCompany",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAccount",
                table: "UserCompany");
        }
    }
}

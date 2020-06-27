using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Users_UserId",
                table: "UserCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaStatuses_Users_UserId",
                table: "VisaStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisaStatuses",
                table: "VisaStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies");

            migrationBuilder.DropColumn(
                name: "InternshipExperienceLevel",
                table: "Internships");

            migrationBuilder.RenameTable(
                name: "VisaStatuses",
                newName: "VisaStatus");

            migrationBuilder.RenameTable(
                name: "UserCompanies",
                newName: "UserCompany");

            migrationBuilder.RenameIndex(
                name: "IX_VisaStatuses_UserId",
                table: "VisaStatus",
                newName: "IX_VisaStatus_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCompanies_UserId",
                table: "UserCompany",
                newName: "IX_UserCompany_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserPhone",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "UserEmailVerified",
                table: "Users",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InternshipPaid",
                table: "Internships",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "UserCompanyLogo",
                table: "UserCompany",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisaStatus",
                table: "VisaStatus",
                column: "VisaStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCompany",
                table: "UserCompany",
                column: "UserCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserEmail",
                table: "Users",
                column: "UserEmail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompany_Users_UserId",
                table: "UserCompany",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaStatus_Users_UserId",
                table: "VisaStatus",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompany_Users_UserId",
                table: "UserCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaStatus_Users_UserId",
                table: "VisaStatus");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserEmail",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisaStatus",
                table: "VisaStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCompany",
                table: "UserCompany");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserCompanyLogo",
                table: "UserCompany");

            migrationBuilder.RenameTable(
                name: "VisaStatus",
                newName: "VisaStatuses");

            migrationBuilder.RenameTable(
                name: "UserCompany",
                newName: "UserCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_VisaStatus_UserId",
                table: "VisaStatuses",
                newName: "IX_VisaStatuses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCompany_UserId",
                table: "UserCompanies",
                newName: "IX_UserCompanies_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserPhone",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "UserEmailVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "InternshipPaid",
                table: "Internships",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternshipExperienceLevel",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisaStatuses",
                table: "VisaStatuses",
                column: "VisaStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies",
                column: "UserCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Users_UserId",
                table: "UserCompanies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaStatuses_Users_UserId",
                table: "VisaStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

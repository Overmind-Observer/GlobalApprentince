using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class add_table_employerLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerLikes",
                columns: table => new
                {
                    EmployerLikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerUserId = table.Column<int>(type: "int", nullable: true),
                    InternUserId = table.Column<int>(type: "int", nullable: true),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerLikes", x => x.EmployerLikeID);
                    table.ForeignKey(
                        name: "FK_EmployerLikes_Users_EmployerUserId",
                        column: x => x.EmployerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployerLikes_Users_InternUserId",
                        column: x => x.InternUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerLikes_EmployerUserId",
                table: "EmployerLikes",
                column: "EmployerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerLikes_InternUserId",
                table: "EmployerLikes",
                column: "InternUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerLikes");
        }
    }
}

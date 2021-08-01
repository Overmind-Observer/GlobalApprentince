using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Global_Intern.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminFirstName = table.Column<string>(nullable: true),
                    AdminLastName = table.Column<string>(nullable: true),
                    AdminEmail = table.Column<string>(nullable: true),
                    AdminPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(nullable: false),
                    UserLastName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: false),
                    UserEmailVerified = table.Column<bool>(nullable: false, defaultValue: false),
                    UserAddress = table.Column<string>(nullable: true),
                    UserCity = table.Column<string>(nullable: true),
                    UserState = table.Column<string>(nullable: true),
                    UserCountry = table.Column<string>(nullable: true),
                    UserZip = table.Column<int>(nullable: false),
                    UserPassword = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    UniqueToken = table.Column<string>(nullable: true),
                    UserPhone = table.Column<string>(nullable: true),
                    UserImage = table.Column<string>(nullable: true),
                    UserGender = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SoftDelete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(nullable: false),
                    CourseType = table.Column<string>(nullable: true),
                    CourseDuration = table.Column<string>(nullable: true),
                    CourseInfo = table.Column<string>(nullable: false),
                    CourseFees = table.Column<string>(nullable: true),
                    CourseExpDate = table.Column<DateTime>(nullable: false),
                    CourseCreatedAt = table.Column<DateTime>(nullable: false),
                    CourseUpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCVName = table.Column<string>(nullable: true),
                    UserCLName = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceTitle = table.Column<string>(nullable: true),
                    ExperienceCompany = table.Column<int>(nullable: false),
                    ExperienceLocation = table.Column<int>(nullable: false),
                    ExperienceStartDate = table.Column<DateTime>(nullable: false),
                    ExperienceEndDate = table.Column<DateTime>(nullable: false),
                    ExperienceStillWorking = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    InternshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipTitle = table.Column<string>(nullable: false),
                    InternshipType = table.Column<string>(nullable: true),
                    InternshipDuration = table.Column<string>(nullable: true),
                    InternshipBody = table.Column<string>(nullable: false),
                    InternshipVirtual = table.Column<bool>(nullable: false),
                    InternshipPaid = table.Column<string>(nullable: true),
                    InternshipEmail = table.Column<string>(nullable: true),
                    InternshipExpDate = table.Column<DateTime>(nullable: false),
                    InternshipCreatedAt = table.Column<DateTime>(nullable: false),
                    InternshipUpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.InternshipId);
                    table.ForeignKey(
                        name: "FK_Internships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilePic = table.Column<string>(nullable: true),
                    ProfileCV = table.Column<string>(nullable: true),
                    ProfileCoverLetter = table.Column<string>(nullable: true),
                    ProfilePersonalStatement = table.Column<string>(nullable: true),
                    ProfileAmbitionSummnary = table.Column<string>(nullable: true),
                    ProfileExperience = table.Column<string>(nullable: true),
                    ProfileRoleFit = table.Column<string>(nullable: true),
                    ProfileAcademicRecord = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationSchool = table.Column<string>(nullable: true),
                    QualificationTitle = table.Column<string>(nullable: true),
                    QualificationType = table.Column<string>(nullable: true),
                    FieldofStudy = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    QualificationStillStudying = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    QualificationLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_Qualifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCL",
                columns: table => new
                {
                    UserCLId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCLFullPath = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCL", x => x.UserCLId);
                    table.ForeignKey(
                        name: "FK_UserCL_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCompany",
                columns: table => new
                {
                    UserCompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCompanyName = table.Column<string>(nullable: true),
                    UserCompanyLogo = table.Column<string>(nullable: true),
                    UserCompanyType = table.Column<string>(nullable: true),
                    UserCompanyInfo = table.Column<string>(nullable: true),
                    UserCompanyAddress = table.Column<string>(nullable: true),
                    UserCompanyState = table.Column<string>(nullable: true),
                    UserCompanyCountry = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompany", x => x.UserCompanyId);
                    table.ForeignKey(
                        name: "FK_UserCompany_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCV",
                columns: table => new
                {
                    UserCVId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCVFullPath = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCV", x => x.UserCVId);
                    table.ForeignKey(
                        name: "FK_UserCV_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    UserDocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(nullable: true),
                    DocumentTitle = table.Column<string>(nullable: true),
                    DocumentPath = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.UserDocumentId);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedInternships",
                columns: table => new
                {
                    AppliedInternshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerStatus = table.Column<string>(nullable: true),
                    TempCVPath = table.Column<string>(nullable: true),
                    TempCLPath = table.Column<string>(nullable: true),
                    DocOnePath = table.Column<string>(nullable: true),
                    DocTwoPath = table.Column<string>(nullable: true),
                    DocThreePath = table.Column<string>(nullable: true),
                    CoverLetterText = table.Column<string>(nullable: true),
                    ExpDate = table.Column<DateTime>(nullable: false),
                    SoftDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    InternshipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedInternships", x => x.AppliedInternshipId);
                    table.ForeignKey(
                        name: "FK_AppliedInternships_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "InternshipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppliedInternships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternStudents",
                columns: table => new
                {
                    InternStudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndemnityInsuranceDetails = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    InternshipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternStudents", x => x.InternStudentId);
                    table.ForeignKey(
                        name: "FK_InternStudents_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "InternshipId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternStudents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MessageFromUserId = table.Column<int>(nullable: true),
                    MessageToUserId = table.Column<int>(nullable: true),
                    MessageTitle = table.Column<string>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true),
                    MessageRead = table.Column<bool>(nullable: false),
                    InternStudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_InternStudents_InternStudentId",
                        column: x => x.InternStudentId,
                        principalTable: "InternStudents",
                        principalColumn: "InternStudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_MessageFromUserId",
                        column: x => x.MessageFromUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_MessageToUserId",
                        column: x => x.MessageToUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedInternships_InternshipId",
                table: "AppliedInternships",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedInternships_UserId",
                table: "AppliedInternships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_UserId",
                table: "Internships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternStudents_InternshipId",
                table: "InternStudents",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternStudents_UserId",
                table: "InternStudents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_InternStudentId",
                table: "Messages",
                column: "InternStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageFromUserId",
                table: "Messages",
                column: "MessageFromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageToUserId",
                table: "Messages",
                column: "MessageToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_UserId",
                table: "Qualifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCL_UserId",
                table: "UserCL",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_UserId",
                table: "UserCompany",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCV_UserId",
                table: "UserCV",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserId",
                table: "UserDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserEmail",
                table: "Users",
                column: "UserEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AppliedInternships");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "UserCL");

            migrationBuilder.DropTable(
                name: "UserCompany");

            migrationBuilder.DropTable(
                name: "UserCV");

            migrationBuilder.DropTable(
                name: "UserDocuments");

            migrationBuilder.DropTable(
                name: "InternStudents");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

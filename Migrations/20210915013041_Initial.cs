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
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmailVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserZip = table.Column<int>(type: "int", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseFees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                name: "Document",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCVName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCLName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Document_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceCompany = table.Column<int>(type: "int", nullable: false),
                    ExperienceLocation = table.Column<int>(type: "int", nullable: false),
                    ExperienceStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExperienceEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExperienceStillWorking = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                    InternshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternshipDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternshipBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipVirtual = table.Column<bool>(type: "bit", nullable: false),
                    InternshipPaid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternshipEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternshipExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternshipCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternshipUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.InternshipId);
                    table.ForeignKey(
                        name: "FK_Internships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileCV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileCoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePersonalStatement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileAmbitionSummnary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileRoleFit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileAcademicRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldofStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QualificationStillStudying = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QualificationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "StudentInternProfiles",
                columns: table => new
                {
                    StudentInternProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentWorkingRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentVisaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentVisaExpire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentIndustryCertificates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentDriverType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentDob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentEthnic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInternProfiles", x => x.StudentInternProfileId);
                    table.ForeignKey(
                        name: "FK_StudentInternProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCL",
                columns: table => new
                {
                    UserCLId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCLFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                    UserCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCompanyCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserCVId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCVFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                name: "AppliedInternships",
                columns: table => new
                {
                    AppliedInternshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempCVPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempCLPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocOnePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocTwoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocThreePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverLetterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    InternshipId = table.Column<int>(type: "int", nullable: true)
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
                    InternStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndemnityInsuranceDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    InternshipId = table.Column<int>(type: "int", nullable: true)
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
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageFromUserId = table.Column<int>(type: "int", nullable: true),
                    MessageToUserId = table.Column<int>(type: "int", nullable: true),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageRead = table.Column<bool>(type: "bit", nullable: false),
                    InternStudentId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_Document_UserId",
                table: "Document",
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
                name: "IX_StudentInternProfiles_UserId",
                table: "StudentInternProfiles",
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
                name: "Document");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "StudentInternProfiles");

            migrationBuilder.DropTable(
                name: "UserCL");

            migrationBuilder.DropTable(
                name: "UserCompany");

            migrationBuilder.DropTable(
                name: "UserCV");

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

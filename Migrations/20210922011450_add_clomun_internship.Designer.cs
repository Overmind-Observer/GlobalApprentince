﻿// <auto-generated />
using System;
using Global_Intern.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Global_Intern.Migrations
{
    [DbContext(typeof(GlobalDBContext))]
    [Migration("20210922011450_add_clomun_internship")]
    partial class add_clomun_internship
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Global_Intern.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Global_Intern.Models.AppliedInternship", b =>
                {
                    b.Property<int>("AppliedInternshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverLetterText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocOnePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocThreePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocTwoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployerStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InternshipId")
                        .HasColumnType("int");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.Property<string>("TempCLPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TempCVPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AppliedInternshipId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("UserId");

                    b.ToTable("AppliedInternships");
                });

            modelBuilder.Entity("Global_Intern.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CourseCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CourseDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CourseExpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CourseFees")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CourseUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Global_Intern.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserCLName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCVName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Global_Intern.Models.EmployerLike", b =>
                {
                    b.Property<int>("EmployerLikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployerUserId")
                        .HasColumnType("int");

                    b.Property<int?>("InternUserId")
                        .HasColumnType("int");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.HasKey("EmployerLikeID");

                    b.HasIndex("EmployerUserId");

                    b.HasIndex("InternUserId");

                    b.ToTable("EmployerLikes");
                });

            modelBuilder.Entity("Global_Intern.Models.Experience", b =>
                {
                    b.Property<int>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExperienceCompany")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExperienceEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExperienceLocation")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExperienceStartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ExperienceStillWorking")
                        .HasColumnType("bit");

                    b.Property<string>("ExperienceTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ExperienceId");

                    b.HasIndex("UserId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("Global_Intern.Models.Institute", b =>
                {
                    b.Property<int>("InstituteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InsituteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstituteLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstituteID");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("Global_Intern.Models.InstituteAdmin", b =>
                {
                    b.Property<int>("InstituteAdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InstituteID")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InstituteAdminID");

                    b.HasIndex("InstituteID");

                    b.HasIndex("UserId");

                    b.ToTable("InstituteAdmins");
                });

            modelBuilder.Entity("Global_Intern.Models.InternStudent", b =>
                {
                    b.Property<int>("InternStudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IndemnityInsuranceDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InternshipId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InternStudentId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("UserId");

                    b.ToTable("InternStudents");
                });

            modelBuilder.Entity("Global_Intern.Models.Internship", b =>
                {
                    b.Property<int>("InternshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InternshipBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InternshipCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("InternshipDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternshipEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InternshipExpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InternshipPaid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternshipTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternshipType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InternshipUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("InternshipVirtual")
                        .HasColumnType("bit");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InternshipId");

                    b.HasIndex("UserId");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("Global_Intern.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InternStudentId")
                        .HasColumnType("int");

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MessageFromUserId")
                        .HasColumnType("int");

                    b.Property<bool>("MessageRead")
                        .HasColumnType("bit");

                    b.Property<string>("MessageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MessageToUserId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("InternStudentId");

                    b.HasIndex("MessageFromUserId");

                    b.HasIndex("MessageToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Global_Intern.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProfileAcademicRecord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileAmbitionSummnary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileCV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileCoverLetter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePersonalStatement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileRoleFit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Global_Intern.Models.Qualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FieldofStudy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualificationLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualificationSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("QualificationStillStudying")
                        .HasColumnType("bit");

                    b.Property<string>("QualificationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualificationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("QualificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("Global_Intern.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Global_Intern.Models.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InternStudentInternProfileId")
                        .HasColumnType("int");

                    b.Property<string>("SkillLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillID");

                    b.HasIndex("InternStudentInternProfileId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Global_Intern.Models.StudentModels.StudentInternProfile", b =>
                {
                    b.Property<int>("StudentInternProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StudentDob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentDriverType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentEthnic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentIndustryCertificates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentVisaExpire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentVisaType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentWorkingRight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StudentInternProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentInternProfiles");
                });

            modelBuilder.Entity("Global_Intern.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("UniqueToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("UserEmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserZip")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCL", b =>
                {
                    b.Property<int>("UserCLId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CLCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCLFullPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserCLId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCL");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCV", b =>
                {
                    b.Property<int>("UserCVId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CVCreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCVFullPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserCVId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCV");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCompany", b =>
                {
                    b.Property<int?>("UserCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserCompanyAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCompanyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserCompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompany");
                });

            modelBuilder.Entity("Global_Intern.Models.AppliedInternship", b =>
                {
                    b.HasOne("Global_Intern.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId");

                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("appliedInternships")
                        .HasForeignKey("UserId");

                    b.Navigation("Internship");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Course", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("Course")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Document", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.EmployerLike", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerUserId");

                    b.HasOne("Global_Intern.Models.User", "Intern")
                        .WithMany()
                        .HasForeignKey("InternUserId");

                    b.Navigation("Employer");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("Global_Intern.Models.Experience", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("Experiences")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.InstituteAdmin", b =>
                {
                    b.HasOne("Global_Intern.Models.Institute", "Institute")
                        .WithMany("InstituteAdmin")
                        .HasForeignKey("InstituteID");

                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("InstituteAdmins")
                        .HasForeignKey("UserId");

                    b.Navigation("Institute");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.InternStudent", b =>
                {
                    b.HasOne("Global_Intern.Models.Internship", "Internship")
                        .WithMany()
                        .HasForeignKey("InternshipId");

                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("InternStudents")
                        .HasForeignKey("UserId");

                    b.Navigation("Internship");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Internship", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Message", b =>
                {
                    b.HasOne("Global_Intern.Models.InternStudent", "InternStudent")
                        .WithMany()
                        .HasForeignKey("InternStudentId");

                    b.HasOne("Global_Intern.Models.User", "MessageFrom")
                        .WithMany()
                        .HasForeignKey("MessageFromUserId");

                    b.HasOne("Global_Intern.Models.User", "MessageTo")
                        .WithMany()
                        .HasForeignKey("MessageToUserId");

                    b.Navigation("InternStudent");

                    b.Navigation("MessageFrom");

                    b.Navigation("MessageTo");
                });

            modelBuilder.Entity("Global_Intern.Models.Profile", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("Profiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Qualification", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("Qualifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Skill", b =>
                {
                    b.HasOne("Global_Intern.Models.StudentModels.StudentInternProfile", "Intern")
                        .WithMany("Skills")
                        .HasForeignKey("InternStudentInternProfileId");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("Global_Intern.Models.StudentModels.StudentInternProfile", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.User", b =>
                {
                    b.HasOne("Global_Intern.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCL", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCV", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.UserCompany", b =>
                {
                    b.HasOne("Global_Intern.Models.User", "User")
                        .WithMany("UserCompanies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Global_Intern.Models.Institute", b =>
                {
                    b.Navigation("InstituteAdmin");
                });

            modelBuilder.Entity("Global_Intern.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Global_Intern.Models.StudentModels.StudentInternProfile", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("Global_Intern.Models.User", b =>
                {
                    b.Navigation("appliedInternships");

                    b.Navigation("Course");

                    b.Navigation("Experiences");

                    b.Navigation("InstituteAdmins");

                    b.Navigation("InternStudents");

                    b.Navigation("Profiles");

                    b.Navigation("Qualifications");

                    b.Navigation("UserCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}

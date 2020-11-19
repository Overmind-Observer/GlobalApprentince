using Global_Intern.Models;
using System;
using System.Linq;

namespace Global_Intern.Data
{
    public static class GlobalDBContextExten
    {
        public static void EnsureDataBaseSeeded(this GlobalDBContext _context)
        {
            int count = _context.Internships.Count();
            if (!_context.Roles.Any())
            {
                _context.AddRange(new Role[]
                    {
                        new Role
                        {
                            RoleName = "student",
                        },
                        new Role
                        {
                            RoleName = "employer",
                        },
                        new Role
                        {
                            RoleName = "teacher",
                        }
                });

                _context.SaveChanges();
            }
            if (!_context.Users.Any())
            {
                var RoleStudent = _context.Roles.SingleOrDefault(a => a.RoleName.Equals("student"));
                var RoleEmployer = _context.Roles.SingleOrDefault(a => a.RoleName.Equals("employer"));
                var RoleTeacher = _context.Roles.SingleOrDefault(a => a.RoleName.Equals("teacher"));

                _context.AddRange(new User[]
                    {
                        new User()
                        {
                            UserFirstName = "Harit",
                            UserLastName = "kumar",
                            UserGender = "male",
                            UserEmail = "harit@b.com",
                            UserPassword = "da3f80f7d570d16967f59bf5edb3ed745cb367c9ce68786fa338ca63f3bd0a6f",
                            Salt = "ae0dvnxmmhbnvq==",
                            UserPhone = "2352366",
                            Role =  RoleEmployer
                        },
                        new User()
                        {
                            UserFirstName = "Cara",
                            UserLastName = "Hess",
                            UserGender = "female",
                            UserEmail = "Cara@b.com",
                            UserPassword = "da3f80f7d570d16967f59bf5edb3ed745cb367c9ce68786fa338ca63f3bd0a6f",
                            Salt = "ae0dvnxmmhbnvq==",
                            UserPhone = "24626",

                            Role = RoleStudent
                        },
                        new User()
                        {
                            UserFirstName = "Sameer",
                            UserLastName = "Kumar",
                            UserGender = "female",
                            UserEmail = "Sameer@b.com",
                            UserPassword = "da3f80f7d570d16967f59bf5edb3ed745cb367c9ce68786fa338ca63f3bd0a6f",
                            Salt = "ae0dvnxmmhbnvq==",
                            UserPhone = "24626",

                            Role = RoleStudent
                        },
                        new User
                        {
                            UserFirstName = "Deepak",
                            UserLastName = "Goyal",
                            UserGender = "male",
                            UserEmail = "deepak@b.com",
                            UserPassword = "da3f80f7d570d16967f59bf5edb3ed745cb367c9ce68786fa338ca63f3bd0a6f",
                            Salt = "ae0dvnxmmhbnvq==",
                            UserPhone = "23526236",

                            Role = RoleTeacher
                        }
                });
                _context.SaveChanges();
            }
            
            var stduent1 = _context.Users.SingleOrDefault(a => a.UserFirstName.Equals("Sameer"));
            var stduent2 = _context.Users.SingleOrDefault(a => a.UserFirstName.Equals("Cara"));
            var employer1 = _context.Users.SingleOrDefault(a => a.UserFirstName.Equals("Harit"));
            var teacher1 = _context.Users.SingleOrDefault(a => a.UserFirstName.Equals("Deepak"));

            if (!_context.Internships.Any())
            {
                _context.AddRange(new Internship[] {
                    new Internship
                    {
                        InternshipTitle = "Software Engineer Intern (.NET)",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "3-6 Months",
                        InternshipBody = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                        "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer " +
                        "took a galley of type and scrambled it to make a type specimen book. It has survived not only five " +
                        "centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was " +
                        "popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more " +
                        "recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        InternshipVirtual = false,
                        InternshipPaid = "IsPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(21),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Developer Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "4 Months",
                        InternshipBody = "Nulla ut tristique nunc. Donec ultrices tortor at justo mattis aliquam. In blandit leo ipsum, " +
                        "quis dictum tortor convallis sit amet. Praesent sodales eget est vel pretium. In finibus fringilla ante, nec " +
                        "dictum est ullamcorper eget.",
                        InternshipVirtual = false,
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(12),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Junior Developer Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "6 Months",
                        InternshipBody = "Fusce pulvinar nisl a quam and typesetting industry. " +
                        "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer " +
                        "took a galley of type and scrambled it to make a type specimen book. It has survived not only five " +
                        "centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was " +
                        "popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more " +
                        "recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        InternshipVirtual = false,
                        InternshipPaid = "IsPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(34),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Full-Stack Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "2-3 Months",
                        InternshipBody = "Donec consectetur cursus varius. Pellentesque at urna suscipit, feugiat elit aliquam, egestas " +
                        "arcu. Cras vestibulum porta leo eget interdum. ",
                        InternshipVirtual = false,
                        InternshipPaid = "NotPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(30),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Junior React Developer",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "6 Months",
                        InternshipBody = "Fusce non tellus nulla. Sed vitae dui est. Donec ullamcorper dapibus turpis eu facilisis. " +
                        "Donec fermentum arcu in scelerisque cursus. In eu laoreet erat. Nunc risus sapien, lobortis ac dictum non, " +
                        "molestie id arcu.",
                        InternshipVirtual = false,
                        InternshipPaid = "IsPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(30),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "DevOp Software Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "5 Months",
                        InternshipBody = "Non vestibulum mi, nec v, fermentum ipsum nec, dictum orci. Fusce ulputate urna. Nunc " +
                        "cursus vulputate lacinia. Vestibulum suscipit orci nulla, quis varius sapien porta sit amet. Proin diam quam, " +
                        "volutpat ac convallis ut, interdum quis augue. Vivamus sollicitudin ante vel est pulvinar,",
                        InternshipVirtual = false,
                        InternshipPaid = "NotPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(30),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "PHP developer Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "2 Months",
                        InternshipBody = "Donec at ipsum ullamcorper, fermentum ipsum nec, dictum orci. Fusce non vestibulum mi, nec " +
                        "vulputate urna. Nunc cursus vulputate lacinia. Vestibulum suscipit orci nulla, quis varius sapien porta sit amet. " +
                        "Proin diam quam, volutpat ac convallis ut, interdum quis augue. Vivamus sollicitudin ante vel est pulvinar,",
                        InternshipVirtual = false,
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(30),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Software Tester Intern",
                        InternshipType = "BA / BI / PROJECT MANAGEMENT",
                        InternshipDuration = "4 Months",
                        InternshipBody = "Fusce faucibus justo quis dictum venenatis. Aliquam ullamcorper nibh quam, ac cursus nulla " +
                        "dignissim venenatis. Etiam et justo libero. Praesent sollicitudin ipsum at malesuada laoreet. Phasellus quis " +
                        "vehicula sem. Duis aliquam dolor sem, et fringilla augue tincidunt ac. Ut a leo vitae diam laoreet tincidunt. ",
                        InternshipVirtual = false,
                        InternshipPaid = "NotPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(12),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Software Engineer Intern (.NET)",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "7 Months",
                        InternshipBody = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                        "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer " +
                        "took a galley of type and scrambled it to make a type specimen book. It has survived not only five " +
                        "centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was " +
                        "popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more " +
                        "recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        InternshipVirtual = false,
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(14),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Quality Assurance Intern",
                        InternshipType = "WEB / PROGRAMMING",
                        InternshipDuration = "12 Months",
                        InternshipBody = "Fusce faucibus justo quis dictum venenatis. Aliquam ullamcorper nibh quam, ac cursus nulla " +
                        "dignissim venenatis. Etiam et justo libero. Praesent sollicitudin ipsum at malesuada laoreet. Phasellus quis " +
                        "vehicula sem. Duis aliquam dolor sem, et fringilla augue tincidunt ac. Ut a leo vitae diam laoreet tincidunt. ",
                        InternshipVirtual = false,
                        InternshipPaid = "IsPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(30),
                        User = employer1
                    },
                    new Internship
                    {
                        InternshipTitle = "Database Developer",
                        InternshipType = "SYSTEMS / OPS / DBA",
                        InternshipDuration = "8 Months",
                        InternshipBody = "Fusce faucibus justo quis dictum venenatis. Aliquam ullamcorper nibh quam, ac cursus " +
                        "nulla dignissim venenatis. Etiam et justo libero. Praesent sollicitudin ipsum at malesuada laoreet. Phasellus " +
                        "quis vehicula sem. Duis aliquam dolor sem, et fringilla augue tincidunt ac. Ut a leo vitae diam laoreet tincidunt. ",
                        InternshipVirtual = false,
                        InternshipPaid = "NotPaid",
                        InternshipCreatedAt = DateTime.UtcNow,
                        InternshipEmail = "nnjkawd@g.com",
                        InternshipExpDate = DateTime.UtcNow.AddDays(7),
                        User = employer1
                    }
                });

                _context.SaveChanges();
            }

            var Intern1 = _context.Internships.Find(1);
            var Intern2 = _context.Internships.Find(2);
            var Intern3 = _context.Internships.Find(3);
            var Intern4 = _context.Internships.Find(4);
            var Intern5 = _context.Internships.Find(5);
            var Intern6 = _context.Internships.Find(6);

            if (!_context.AppliedInternships.Any())
            {
                _context.AddRange(new AppliedInternship[] {
                    new AppliedInternship
                    {
                        EmployerStatus = "Viewing",
                        User = stduent1,
                        Internship = Intern3
                    },
                    new AppliedInternship
                    {
                        EmployerStatus = "Viewing",
                        User = stduent1,
                        Internship = Intern1
                    },
                    new AppliedInternship
                    {
                        EmployerStatus = "Viewing",
                        User = stduent2,
                        Internship = Intern1
                    },
                    new AppliedInternship
                    {
                        EmployerStatus = "Selected",
                        User = stduent1,
                        Internship = Intern4
                    },
                    new AppliedInternship
                    {
                        EmployerStatus = "Viewing",
                        User = stduent1,
                        Internship = Intern1
                    },
                    new AppliedInternship
                    {
                        EmployerStatus = "Not Seleted",
                        User = stduent1,
                        Internship = Intern6
                    }
                });
            }
            _context.SaveChanges();
        }
    }
}

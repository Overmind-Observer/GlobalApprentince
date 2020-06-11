namespace Global_Intern.Models
{
    public class AccountLogin
    {
        public string   Email { get; set; }
        public string   Password { get; set; }
        public bool     RememberMe { get; set; }
    }

    public class AccountRegister
    {
        public string   FirstName { get; set; }
        public string   LastName { get; set; }
        public string   Gender { get; set; }
        public string   Email { get; set; }
        public string   Password { get; set; }
        public string   ConfirmPassword { get; set; }
        public int      UserRole { get; set; }
        public int      Phone { get; set; }
    }
}

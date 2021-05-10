namespace TestAutomation.Framework.Common
{
    public class UserProfile
    {
        public BasicProfile BasicProfile { get; set; }
        public AdditionalProfile AdditionalProfile { get; set; }
        public ChangePassword ChangePassword { get; set; }
    }

    public class BasicProfile
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AdditionalProfile
    {
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Hobby Hobby { get; set; }
    }

    public class ChangePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

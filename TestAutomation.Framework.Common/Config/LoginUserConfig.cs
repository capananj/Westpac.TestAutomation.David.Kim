using System.Configuration;

namespace TestAutomation.Framework.Common
{
    public class LoginUserConfig : ConfigurationSection
    {
        [ConfigurationProperty("UserType")] public UserType UserType => (UserType)this["UserType"];
        [ConfigurationProperty("Username")] public string Username => (string)this["Username"];
        [ConfigurationProperty("Password")] public string Password => (string)this["Password"];
        [ConfigurationProperty("FirstName")] public string FirstName => (string)this["FirstName"];
        [ConfigurationProperty("LastName")] public string LastName => (string)this["LastName"];
    }
}

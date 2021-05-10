using System;
using TestAutomation.Framework.Common;

namespace TestAutomation.Framework.UI
{
    public class UserFactory
    {
        public static LoginUser Create(UserType userType)
        {
            var dateTime = DateTime.Now.ToString("-MMdd_HHmmssff");
            var username = $"testuser{dateTime}";
            var firstName = $"Firstname{dateTime}";
            var lastName = $"Lastname{dateTime}";
            var password = "Passw0rd!";

            var loginUser = new LoginUser()
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                UserType = userType
            };

            return loginUser;
        }
    }
}

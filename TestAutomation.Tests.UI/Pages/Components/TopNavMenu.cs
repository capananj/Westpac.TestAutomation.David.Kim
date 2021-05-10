using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages.Components
{
    public class TopNavMenu : BaseComponent
    {
        public override List<IWebElement> ErrorMessages => FluentFindElements(By.CssSelector("span.label-warning"), false);
        public IWebElement UsernameTextfield => FluentFindElement(By.Name("login"));
        public IWebElement PasswordTextfield => FluentFindElement(By.Name("password"));
        public IWebElement NameTextfield => FluentFindElement(By.CssSelector("span.nav-link.disabled"), false);
        public IWebElement LoginButton => FluentFindElement(By.CssSelector("button.btn-success[type='submit']"));
        public IWebElement ProfileLink => FluentFindElement(By.XPath("//a[text()='Profile']"));
        public IWebElement LogoutLink => FluentFindElement(By.XPath("//a[text()='Logout']"));
        public IWebElement RegisterButton => FluentFindElement(By.CssSelector("a.btn-success-outline"));
        public IWebElement BrandLink => FluentFindElement(By.CssSelector("a.navbar-brand"));

        public TopNavMenu(RemoteWebDriver driver, double timeout) : base(driver, timeout)
        {
        }

        public override List<string> GetErrorMessages()
        {
            return ErrorMessages.Select(x => x.Text).ToList();
        }

        public TopNavMenu Login(string username, string password)
        {
            UsernameTextfield.ClearAndSendKeys(username);
            PasswordTextfield.ClearAndSendKeys(password);
            LoginButton.Click();
            return this;
        }

        public RegisterPage ClickRegister()
        {
            RegisterButton.Click();
            return new RegisterPage(Driver, DriverTimeout);
        }

        public TopNavMenu ClickLogout()
        {
            LogoutLink.Click();
            return this;
        }

        public ProfilePage ClickProfile()
        {
            ProfileLink.Click();
            return PageFactory.Create<ProfilePage>(Driver, DriverTimeout);
        }

        public HomePage ClickBrandLink()
        {
            BrandLink.Click();
            return PageFactory.Create<HomePage>(Driver, DriverTimeout);
        }

        public string GetUserName()
        {
            return NameTextfield?.Text;
        }

        public string GetHtmlValidationMessage(string fieldName)
        {
            var message = "";
            if (fieldName == "Username")
            {
                message = UsernameTextfield.GetAttribute("validationMessage");
            }
            else if (fieldName == "Password")
            {
                message = PasswordTextfield.GetAttribute("validationMessage");
            }

            return message;
        }

        public bool IsLoggedIn(string firstname = null)
        {
            var loggedInUsername = NameTextfield;
            var result = loggedInUsername != null;

            if (firstname == null)
            {
                result = result && loggedInUsername.Text.Contains("Hi");
            }
            else
            {
                result = result && loggedInUsername.Text == $"Hi, {firstname}";
            }

            return result;
        }
    }
}

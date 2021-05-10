using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages
{
    public class RegisterPage : BasePage
    {
        protected IWebElement LoginTextField => FluentFindElement(By.Id("username"));
        protected IWebElement FirstNameTextField => FluentFindElement(By.Id("firstName"));
        protected IWebElement LastNameTextField => FluentFindElement(By.Id("lastName"));
        protected IWebElement PasswordTextField => FluentFindElement(By.Id("password"));
        protected IWebElement ConfirmPasswordTextField => FluentFindElement(By.Id("confirmPassword"));
        protected IWebElement RegisterButton => FluentFindElement(By.XPath("//button[text()='Register']"));
        protected IWebElement CancelLink => FluentFindElement(By.XPath("//a[text()='Cancel']"));
        protected IWebElement RegistrationSuccessfulMessage => FluentFindElement(By.CssSelector("div.result.alert-success"), false);
        public RegisterPage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/register";
        }

        public RegisterPage RegisterUser(LoginUser user, bool submitForm = true)
        {
            LoginTextField.ClearAndSendKeys(user.Username);
            FirstNameTextField.ClearAndSendKeys(user.FirstName);
            LastNameTextField.ClearAndSendKeys(user.LastName);
            PasswordTextField.ClearAndSendKeys(user.Password);
            ConfirmPasswordTextField.ClearAndSendKeys(user.Password);

            if (submitForm)
            {
                RegisterButton.Click();
            }

            return this;
        }

        public RegisterPage ClickRegisterButton()
        {
            RegisterButton.Click();
            return this;
        }

        public RegisterPage ClickCancelLink()
        {
            CancelLink.Click();
            return this;
        }

        public RegisterPage RemoveFieldValues()
        {
            LoginTextField.SendKeys(Keys.Backspace);
            FirstNameTextField.SendKeys(Keys.Backspace);
            LastNameTextField.SendKeys(Keys.Backspace);
            PasswordTextField.SendKeys(Keys.Backspace);
            ConfirmPasswordTextField.SendKeys(Keys.Backspace);
            return this;
        }

        public RegisterPage EnterConfirmPassword(string value)
        {
            ConfirmPasswordTextField.SendKeys(value);
            return this;
        }
        public bool IsSuccessfulMessageDisplayed()
        {
            return RegistrationSuccessfulMessage != null;
        }

        public bool IsRegisterButtonEnabled()
        {
            return RegisterButton.Enabled;
        }
    }
}

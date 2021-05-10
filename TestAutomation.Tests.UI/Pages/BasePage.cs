using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using TestAutomation.Framework.UI;
using TestAutomation.Tests.UI.Pages.Components;

namespace TestAutomation.Tests.UI.Pages
{
    public class BasePage
    {
        protected virtual List<IWebElement> ErrorMessages => FluentFindElements(By.CssSelector("div.alert.alert-danger:not([hidden])"));
        public string CurrentUrl => Driver.Url;
        public string UrlPath { get; set; }
        public TopNavMenu TopNavMenu { get; set; }
        public RemoteWebDriver Driver { get; set; }
        public double DriverTimeout { get; set; }
        public BasePage(RemoteWebDriver driver, double driverTimeout)
        {
            Driver = driver;
            DriverTimeout = driverTimeout;
            TopNavMenu = PageFactory.Create<TopNavMenu>(driver, driverTimeout);
        }

        public IWebElement FluentFindElement(By by, bool failIfNotFound = true, double timeoutMilliseconds = 0)
        {
            var timeout = timeoutMilliseconds > 0 ? timeoutMilliseconds : DriverTimeout;
            return Driver.FluentFindElement(by, failIfNotFound, timeout);
        }

        public List<IWebElement> FluentFindElements(By by, bool failIfNotFound = true, double timeoutMilliseconds = 0)
        {
            var timeout = timeoutMilliseconds > 0 ? timeoutMilliseconds : DriverTimeout;
            return Driver.FluentFindElements(by, failIfNotFound, timeout);
        }

        public virtual string GetErrorMessage(string errorMessage)
        {
            return GetErrorMessages()?.FirstOrDefault(x => x == errorMessage);
        }

        public virtual List<string> GetErrorMessages()
        {
            return ErrorMessages?.Select(x => x.Text).ToList();
        }
    }
}

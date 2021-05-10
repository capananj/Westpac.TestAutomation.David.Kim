using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages
{
    public class HomePage : BasePage
    {
        protected IWebElement OverallImage => FluentFindElement(By.CssSelector("a[href='/overall']"));
        protected IWebElement MakeImage => FluentFindElement(By.XPath("//div[h2[text()='Popular Make']]/a"));
        public HomePage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/";
        }

        public OverallPage ClickOverallImage()
        {
            OverallImage.Click();
            return PageFactory.Create<OverallPage>(Driver, DriverTimeout);
        }
        
        public MakePage ClickMakeImage()
        {
            MakeImage.Click();
            return PageFactory.Create<MakePage>(Driver, DriverTimeout);
        }
    }
}
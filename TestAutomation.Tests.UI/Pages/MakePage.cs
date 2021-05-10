using OpenQA.Selenium.Remote;

namespace TestAutomation.Tests.UI.Pages
{
    public class MakePage : BasePage
    {
        public MakePage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/make";
        }
    }
}
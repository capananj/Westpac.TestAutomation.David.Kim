using AventStack.ExtentReports;
using OpenQA.Selenium.Remote;
using TestAutomation.Framework.Common;

namespace TestAutomation.Framework.UI
{
    public class UITestContext : BaseTestContext
    {
        public RemoteWebDriver Driver { get; set; }
        public double DriverTimeout { get; set; }
        public BrowserType BrowserType { get; set; }
        public ExtentTest LastStep { get; set; }
        public string LastStepScreenshot { get; set; }
    }
}

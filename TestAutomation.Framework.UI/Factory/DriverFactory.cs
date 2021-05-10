using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Microsoft.Edge.SeleniumTools;

namespace TestAutomation.Framework.UI
{
    public static class DriverFactory
    {
        public static RemoteWebDriver Create(BrowserType driverType, bool headless = false)
        {
            var headlessArg = "--headless";
            RemoteWebDriver driver;
            switch (driverType)
            {
                case BrowserType.Chrome:
                    var chromeOption = new ChromeOptions();
                    if (headless)
                    {
                        chromeOption.AddArgument(headlessArg);
                    }
                    driver = new ChromeDriver(chromeOption);

                    break;
                case BrowserType.Firefox:
                    var firefoxOption = new FirefoxOptions();
                    if (headless)
                    {
                        firefoxOption.AddArgument(headlessArg);
                    }
                    driver = new FirefoxDriver(firefoxOption);

                    break;
                case BrowserType.Edge:
                    var edgeOption = new EdgeOptions();
                    if (headless)
                    {
                        edgeOption.UseChromium = true;
                        edgeOption.AddArgument(headlessArg);
                    }
                    driver = new EdgeDriver(edgeOption);

                    break;
                default:
                    throw new ArgumentException($"Driver type not supported: {driverType}");
            }

            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}

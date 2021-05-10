using System;
using OpenQA.Selenium.Remote;

namespace TestAutomation.Framework.UI
{
    public class PageFactory
    {
        public static T Create<T>(RemoteWebDriver driver, double driverTimeout)
        {
            return (T)Activator.CreateInstance(typeof(T), driver, driverTimeout);
        }
    }
}

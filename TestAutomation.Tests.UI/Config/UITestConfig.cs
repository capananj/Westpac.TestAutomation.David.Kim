using System.Configuration;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Config
{
    public class UITestConfig : BaseTestConfig
    {
        [ConfigurationProperty("Browser")] 
        public BrowserType BrowserType => (BrowserType)this["Browser"];
        
        [ConfigurationProperty("DriverTimeoutMilliseconds")] 
        public double DriverTimeoutMilliseconds => (double)this["DriverTimeoutMilliseconds"];
    }
}

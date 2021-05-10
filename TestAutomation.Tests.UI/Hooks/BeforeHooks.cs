using System.Collections.Generic;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;
using TestAutomation.Tests.UI.Config;

namespace TestAutomation.Tests.UI.Hooks
{
    [Binding]
    public class BeforeHooks
    {
        private static UITestConfig _testConfig;
        private static LoginUserConfig _adminUserConfig;
        private static LoginUserConfig _businessUserConfig;
        private static ExtentReporter _reporter;

        [BeforeTestRun]
        public static void InitializeTestConfig()
        {
            _testConfig = TestConfigLoader.Load<UITestConfig>("UITestConfig");
            _adminUserConfig = TestConfigLoader.Load<LoginUserConfig>("AdminUserConfig");
            _businessUserConfig = TestConfigLoader.Load<LoginUserConfig>("BusinessUserConfig");
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            _reporter = new ExtentReporter(_testConfig.ReportName, _testConfig.Environment, _testConfig.ReportFileName);
        }

        [BeforeFeature(Order = 0)]
        public static void InitializeFeatureContext(FeatureContext featureContext)
        {
            var adminUser = new LoginUser()
            {
                UserType = _adminUserConfig.UserType,
                Username = _adminUserConfig.Username,
                Password = _adminUserConfig.Password,
                FirstName = _adminUserConfig.FirstName,
                LastName = _adminUserConfig.LastName
            };

            var businessUser = new LoginUser()
            {
                UserType = _businessUserConfig.UserType,
                Username = _businessUserConfig.Username,
                Password = _businessUserConfig.Password,
                FirstName = _businessUserConfig.FirstName,
                LastName = _businessUserConfig.LastName
            };
            
            var driver = DriverFactory.Create(_testConfig.BrowserType);

            var testContext = new UITestContext()
            {
                BaseUrl = _testConfig.Url,
                BrowserType = _testConfig.BrowserType,
                Driver = driver,
                DriverTimeout = _testConfig.DriverTimeoutMilliseconds,
                Reporter = _reporter,
                UserCredentials = new List<LoginUser>
                {
                    adminUser,
                    businessUser
                },
            };

            driver.Navigate().GoToUrl(testContext.BaseUrl);
            _reporter.CreateFeature(featureContext.FeatureInfo.Title);

            featureContext.FeatureContainer.RegisterInstanceAs(testContext);
        }

        [BeforeScenario(Order = 0)]
        public void InitializeScenarioExtent(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var testContext = featureContext.FeatureContainer.Resolve<UITestContext>();
            testContext.Reporter.CreateScenario(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);
        }


        [BeforeScenario(Order = 1)]
        public void InitializeScenarioContext(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var testContext = featureContext.FeatureContainer.Resolve<UITestContext>();
            scenarioContext.ScenarioContainer.RegisterInstanceAs(testContext);
        }
    }
}

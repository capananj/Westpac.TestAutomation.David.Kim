using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using TestAutomation.Framework.UI;
using FluentAssertions;
using AventStack.ExtentReports.Gherkin.Model;

namespace TestAutomation.Tests.UI.Steps
{
    public class BaseStep
    {
        public HomePage HomePage;
        public RemoteWebDriver Driver { get; set; }
        public FeatureContext FeatureContext { get; set; }
        public ScenarioContext ScenarioContext { get; set; }
        public UITestContext TestContext { get; set; }

        public BaseStep(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContext = featureContext;
            ScenarioContext = scenarioContext;
            TestContext = featureContext.FeatureContainer.Resolve<UITestContext>();
            ScenarioContext.Set(Driver, "Driver");

            if (Driver == null)
            {
                Driver = TestContext.Driver;
            }

            HomePage = PageFactory.Create<HomePage>(Driver, TestContext.DriverTimeout);
        }

        [Then(@"I should see an error message (.*)")]
        public virtual void ThenIShouldSeeAnErrorMessage(string errorMessage)
        {
            HomePage
                .GetErrorMessage(errorMessage)
                .Should()
                .Be(errorMessage, "Field is empty");
        }
    }
}

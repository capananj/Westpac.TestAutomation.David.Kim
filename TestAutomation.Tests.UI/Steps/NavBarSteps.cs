using FluentAssertions;
using TechTalk.SpecFlow;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding, Scope(Tag = "NavBar")]
    public class NavBarSteps : BaseStep
    {
        public NavBarSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
        }

        [When(@"I click brand link")]
        public void WhenIClickBrandLink()
        {
            HomePage
                .TopNavMenu
                .ClickBrandLink();
        }

        [Then(@"I should be redirected to Home page")]
        public void ThenIShouldBeRedirectedToHomePage()
        {
            var homePageUrl = $"{TestContext.BaseUrl}{HomePage.UrlPath}";
            
            HomePage
                .CurrentUrl
                .Should()
                .Be(homePageUrl);
        }

    }
}
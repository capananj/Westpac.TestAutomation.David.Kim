using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using FluentAssertions;
using TestAutomation.Framework.Common;
using System.Linq;
using System;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding, Scope(Tag = "Login")]
    public class LoginSteps : BaseStep
    {
        public LoginSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
        }


        [When(@"I login as (Admin|Business|Internal|Customer) user")]
        public void WhenILoginAsUser(UserType userType)
        {
            var user =
                TestContext
                .UserCredentials
                .Single(x => x.UserType == userType);

            HomePage
                .TopNavMenu
                .Login(user.Username, user.Password);

            TestContext.LoginUser = user;
        }

        [Then(@"I should be able to see a greeting message on Page")]
        public void ThenIShouldBeAbleToSeeGreetingMessageOnPage()
        {
            HomePage
                .TopNavMenu
                .GetUserName()
                .Should()
                .Be($"Hi, {TestContext.LoginUser.FirstName}");
        }

        [Then(@"I should see an error message (.*)")]
        public override void ThenIShouldSeeAnErrorMessage(string errorMessage)
        {
            HomePage
                .TopNavMenu
                .GetErrorMessage(errorMessage)
                .Should()
                .NotBeNullOrEmpty()
                .And
                .Be(errorMessage);
        }

        [Then(@"I should see a validation error message for (.*) field")]
        public void ThenIShouldSeeValidationErrorMessage(string fieldName)
        {
            if (!fieldName.MatchesAny("Username", "Password"))
            {
                throw new Exception("ThenIShouldSeeValidationErrorMessage() supports Username and Password field only");
            }

            var message = HomePage.TopNavMenu.GetHtmlValidationMessage(fieldName);

            message
                .Should()
                .NotBeNullOrEmpty();
        }

        [When(@"I click logout link")]
        public void WhenIClickLogOutLink()
        {
            HomePage
                .TopNavMenu
                .ClickLogout();
        }

        [Then(@"I should be loggeed out")]
        public void ThenIShouldBeLoggeedOut()
        {
            HomePage
                .TopNavMenu
                .IsLoggedIn()
                .Should()
                .BeFalse();
        }

    }
}
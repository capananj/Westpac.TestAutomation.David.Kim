using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using FluentAssertions;
using TestAutomation.Framework.UI;
using TestAutomation.Framework.Common;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding, Scope(Tag = "Register")]
    public class RegisterSteps : BaseStep
    {
        public RegisterPage RegisterPage { get; set; }
        public RegisterSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
            RegisterPage = PageFactory.Create<RegisterPage>(Driver, TestContext.DriverTimeout);
        }

        [When(@"I enter (a Valid|an Invalid) user details in Register page")]
        public void WhenIEnterUserDetailsInRegisterPage(string dataValidity)
        {
            var loginUser = UserFactory.Create(UserType.Admin);

            if(dataValidity == "an Invalid")
            {
                loginUser.Username = "a";
                loginUser.FirstName= "a";
                loginUser.LastName = "a";
                loginUser.Password = "a";
            }

            RegisterPage.RegisterUser(loginUser, false);
            
            TestContext.LoginUser = loginUser;
        }

        [When(@"I remove the user details in Register page")]
        public void WhenIRemoveUserDetailsInRegisterPage()
        {
            RegisterPage.RemoveFieldValues();
        }

        [When(@"I enter an extra letter in Confirm Password field")]
        public void WhenIEnterExtraLetterInConfirmPasswordField()
        {
            var extraValue = "abc";
            RegisterPage.EnterConfirmPassword(extraValue);
        }

        [When(@"I click (Register|Cancel) button")]
        public void WhenIClickRegisterButton(string buttonName)
        {
            if (buttonName == "Register")
            {
                RegisterPage.ClickRegisterButton();
            }
            else
            {
                RegisterPage.ClickCancelLink();
            }
        }

        [Then(@"I should see the Registration successful message")]
        public void ThenIShouldSeeTheRegistrationSuccessfulMessage()
        {
            RegisterPage.IsSuccessfulMessageDisplayed().Should().BeTrue();
        }

        [Then(@"I should be able to login with newly created user credential")]
        public void ThenIShouldBeAbleToLoginWithNewlyCreatedUserCredential()
        {
            var loginUser = TestContext.LoginUser;
            RegisterPage
                .TopNavMenu
                .Login(loginUser.Username, loginUser.Password);

            RegisterPage
                .TopNavMenu
                .IsLoggedIn(loginUser.FirstName)
                .Should()
                .BeTrue();
        }


        [Then(@"Register button should be (Enabled|Disabled)")]
        public void ThenRegisterButtonShouldRemainDisabled(string isEnabled)
        {
            if (isEnabled == "Enabled")
            {
                RegisterPage
                    .IsRegisterButtonEnabled()
                    .Should()
                    .BeTrue();

            }
            else
            {
                RegisterPage
                    .IsRegisterButtonEnabled()
                    .Should()
                    .BeFalse();
            }
        }

    }
}
using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using FluentAssertions;
using TestAutomation.Framework.UI;
using OpenQA.Selenium;
using System;
using TestAutomation.Framework.Common;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding, Scope(Tag = "Profile")]
    public class ProfileSteps : BaseStep
    {
        public ProfilePage ProfilePage { get; set; }
        public ProfileSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
            ProfilePage = PageFactory.Create<ProfilePage>(Driver, TestContext.DriverTimeout);
        }



        [When(@"I remove (First Name|Last Name) value in Profile page")]
        public void WhenIRemoveFieldValueInProfilePage(string nameField)
        {
            var singleLetter = "a";
            if (nameField == "First Name")
            {
                ProfilePage.FirstNameTextField.ClearAndSendKeys(singleLetter);
                ProfilePage.FirstNameTextField.SendKeys(Keys.Backspace);
            }
            else
            {
                ProfilePage.LastNameTextField.ClearAndSendKeys(singleLetter);
                ProfilePage.LastNameTextField.SendKeys(Keys.Backspace);
            }
        }

        [When(@"I update user profile")]
        public void WhenIUpdateUserProfile()
        {
            var profile = ProfilePage.GetProfile();
            ScenarioContext["PreviousProfile"] = profile;

            var now = DateTime.Now.ToString("hhmmss");
            var newProfile = new UserProfile
            {
                BasicProfile = new BasicProfile
                {
                    Login = profile.BasicProfile.Login,
                    FirstName = profile.BasicProfile.FirstName + now,
                    LastName = profile.BasicProfile.LastName + now
                },
                AdditionalProfile = new AdditionalProfile
                {
                    Address = profile.AdditionalProfile.Address + now,
                    Age = profile.AdditionalProfile.Age + 1,
                    Gender = profile.AdditionalProfile.Gender + now,
                    Hobby = profile.AdditionalProfile.Hobby + 1,
                    Phone = "0210000000"
                },
                ChangePassword = new ChangePassword
                {
                    CurrentPassword = TestContext.LoginUser.Password,
                    NewPassword = TestContext.LoginUser.Password + 1,
                    ConfirmPassword = TestContext.LoginUser.Password + 1,
                }
            };

            ProfilePage.UpdateProfile(newProfile);
            
            ScenarioContext["NewProfile"] = newProfile;

            TestContext.LoginUser.Password = newProfile.ChangePassword.NewPassword;
        }

        [Then(@"I should see that the user details are updated")]
        public void ThenIShouldSeeThatTheUserDetailsAreUpdated()
        {
            var expectedProfile = (UserProfile)ScenarioContext["NewProfile"];
            var actualProfile = ProfilePage.GetProfile();

            actualProfile.BasicProfile.Should().BeEquivalentTo(expectedProfile.BasicProfile);
            actualProfile.AdditionalProfile.Should().BeEquivalentTo(expectedProfile.AdditionalProfile);
        }

        [Then(@"Save button should be (Enabled|Disabled)")]
        public void ThenSaveButtonShouldBeDisabled(string isEnabled)
        {
            if (isEnabled == "Enabled")
            {
                ProfilePage
                    .IsSaveButtonEnabled()
                    .Should()
                    .BeTrue();
            }
            else
            {
                ProfilePage
                    .IsSaveButtonEnabled()
                    .Should()
                    .BeFalse();
            }
        }

        [Then(@"I should see the Profile Update successful message")]
        public void ThenIShouldSeeTheProfileUpdateSuccessfulMessage()
        {
            ProfilePage
                .IsSuccessfulMessageDisplayed()
                .Should()
                .BeTrue();
        }

    }
}
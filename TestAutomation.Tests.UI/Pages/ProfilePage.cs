using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages
{
    public class ProfilePage : BasePage
    {
        public IWebElement FirstNameTextField => FluentFindElement(By.Id("firstName"));
        public IWebElement LastNameTextField => FluentFindElement(By.Id("lastName"));
        protected IWebElement UserNameTextField => FluentFindElement(By.Id("username"));
        protected IWebElement GenderDropdown => FluentFindElement(By.Id("gender"));
        protected IWebElement AgeTextField => FluentFindElement(By.Id("age"));
        protected IWebElement AddressTextField => FluentFindElement(By.Id("address"));
        protected IWebElement PhoneTextField => FluentFindElement(By.Id("phone"));
        protected SelectElement HobbyDropdown => new SelectElement(FluentFindElement(By.Id("hobby")));
        protected IWebElement CurrentPasswordTextField => FluentFindElement(By.Id("currentPassword"));
        protected IWebElement NewPasswordTextField => FluentFindElement(By.Id("newPassword"));
        protected IWebElement ConfirmPasswordTextField => FluentFindElement(By.Id("newPasswordConfirmation"));
        protected IWebElement SaveButton => FluentFindElement(By.CssSelector("button.btn-default"));
        protected IWebElement CancelLink => FluentFindElement(By.CssSelector("a.btn[role='button']"));
        protected IWebElement UpdateSuccessfulMessage => FluentFindElement(By.CssSelector("div.result.alert-success.hidden-md-down"), false);

        public ProfilePage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/profile";
        }

        public UserProfile GetProfile()
        {
            var profile = new UserProfile
            {
                BasicProfile = new BasicProfile
                {
                    Login = UserNameTextField.GetAttribute("value"),
                    FirstName = FirstNameTextField.GetAttribute("value"),
                    LastName = LastNameTextField.GetAttribute("value")
                },
                AdditionalProfile = new AdditionalProfile
                {
                    Address = AddressTextField.GetAttribute("value"),
                    Gender = GenderDropdown.GetAttribute("value"),
                    Phone = PhoneTextField.GetAttribute("value")
                }
            };

            var age = AgeTextField.GetAttribute("value");
            if (!age.IsNullOrEmpty())
            {
                profile.AdditionalProfile.Age = int.Parse(age);
            }

            if (HobbyDropdown.AllSelectedOptions.Count > 0)
            {
                profile.AdditionalProfile.Hobby = (Hobby)Enum.Parse(typeof(Hobby), HobbyDropdown.SelectedOption.Text);
            }
            return profile;
        }

        public ProfilePage UpdateProfile(UserProfile userProfile, bool submitForm = true)
        {
            if (userProfile != null)
            {
                if (userProfile.BasicProfile != null)
                {
                    UpdateBasicProfile(userProfile.BasicProfile);
                }

                if (userProfile.AdditionalProfile != null)
                {
                    UpdateAdditionalProfile(userProfile.AdditionalProfile);
                }

                if (userProfile.ChangePassword != null)
                {
                    UpdatePassword(userProfile.ChangePassword);
                }
            }

            if (submitForm)
            {
                ClickSaveButton();
            }

            return this;
        }

        private ProfilePage UpdateBasicProfile(BasicProfile basicProfile)
        {
            FirstNameTextField.ClearAndSendKeys(basicProfile.FirstName);
            LastNameTextField.ClearAndSendKeys(basicProfile.LastName);
            return this;
        }

        private ProfilePage UpdateAdditionalProfile(AdditionalProfile additionalProfile)
        {
            GenderDropdown.ClearAndSendKeys(additionalProfile.Gender);
            AgeTextField.ClearAndSendKeys(additionalProfile.Age);
            AddressTextField.ClearAndSendKeys(additionalProfile.Address);
            PhoneTextField.ClearAndSendKeys(additionalProfile.Phone);
            HobbyDropdown.SelectByText(additionalProfile.Hobby.ToString());
            return this;
        }

        private ProfilePage UpdatePassword(ChangePassword changePassword)
        {
            CurrentPasswordTextField.ClearAndSendKeys(changePassword.CurrentPassword);
            NewPasswordTextField.ClearAndSendKeys(changePassword.NewPassword);
            ConfirmPasswordTextField.ClearAndSendKeys(changePassword.ConfirmPassword);

            return this;
        }

        public ProfilePage ClickSaveButton()
        {
            SaveButton.Click();
            return this;
        }

        public ProfilePage ClickCancelLink()
        {
            CancelLink.Click();
            return this;
        }

        public bool IsSuccessfulMessageDisplayed()
        {
            return UpdateSuccessfulMessage != null;
        }

        public bool IsSaveButtonEnabled()
        {
            return SaveButton.Enabled;
        }
    }
}
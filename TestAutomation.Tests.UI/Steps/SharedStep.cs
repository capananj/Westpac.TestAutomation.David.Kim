using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using TestAutomation.Framework.UI;
using FluentAssertions;
using AventStack.ExtentReports.Gherkin.Model;
using TestAutomation.Framework.Common;
using System.Linq;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding]
    public class SharedStep
    {
        public HomePage HomePage;
        public OverallPage OverallPage;
        public ModelDetailsPage ModelDetailsPage;
        public ProfilePage ProfilePage;
        public RegisterPage RegisterPage;
        public MakePage MakePage;
        public RemoteWebDriver Driver { get; set; }
        public FeatureContext FeatureContext { get; set; }
        public ScenarioContext ScenarioContext { get; set; }
        public UITestContext TestContext { get; set; }

        public SharedStep(FeatureContext featureContext, ScenarioContext scenarioContext)
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
            OverallPage = PageFactory.Create<OverallPage>(Driver, TestContext.DriverTimeout);
            ModelDetailsPage = PageFactory.Create<ModelDetailsPage>(Driver, TestContext.DriverTimeout);
            ProfilePage = PageFactory.Create<ProfilePage>(Driver, TestContext.DriverTimeout);
            RegisterPage = PageFactory.Create<RegisterPage>(Driver, TestContext.DriverTimeout);
            MakePage = PageFactory.Create<MakePage>(Driver, TestContext.DriverTimeout);
        }

        [Given(@"I am on (.*) page")]
        public void GivenIAmOnAPage(string pageName)
        {
            if (pageName.CompareSanitized("Home"))
            {
                GivenIAmOnHomePage();
            }
            else if (pageName.CompareSanitized("Model Details"))
            {
                GivenIAmOnModelDetailsPage();
            }
            else if (pageName.CompareSanitized("Overall"))
            {
                GivenIAmOnOverallPage();
            }
            else if (pageName.CompareSanitized("Profile"))
            {
                GivenIAmOnProfilePage();
            }
            else if (pageName.CompareSanitized("Register"))
            {
                GivenIAmOnRegisterPage();
            }
            else if (pageName.CompareSanitized("Make"))
            {
                GivenIAmOnMakePage();
            }
        }

        public void GivenIAmOnHomePage()
        {
            var homePageUrl = $"{TestContext.BaseUrl}{HomePage.UrlPath}";
            if (HomePage.CurrentUrl != homePageUrl)
            {
                HomePage
                    .TopNavMenu
                    .ClickBrandLink();
            }

            HomePage
                .CurrentUrl
                .Should()
                .Be(homePageUrl);
        }

        public void GivenIAmOnRegisterPage()
        {
            var registerPageUrl = $"{TestContext.BaseUrl}{RegisterPage.UrlPath}";

            if (RegisterPage.CurrentUrl != registerPageUrl)
            {
                HomePage
                    .TopNavMenu
                    .ClickRegister();
            }

            RegisterPage
                .CurrentUrl
                .Should()
                .Be(registerPageUrl);
        }

        public void GivenIAmOnProfilePage()
        {
            var profilePageUrl = $"{TestContext.BaseUrl}{ProfilePage.UrlPath}";

            if (ProfilePage.CurrentUrl != profilePageUrl)
            {
                HomePage
                    .TopNavMenu
                    .ClickProfile();
            }

            ProfilePage
                .CurrentUrl
                .Should()
                .Be(profilePageUrl);
        }

        public void GivenIAmOnOverallPage()
        {
            var overallPageUrl = $"{TestContext.BaseUrl}{OverallPage.UrlPath}";
            if (!OverallPage.CurrentUrl.Contains(overallPageUrl))
            {
                HomePage
                    .TopNavMenu
                    .ClickBrandLink()
                    .ClickOverallImage();
            }

            OverallPage
                .CurrentUrl
                .Should()
                .Contain(overallPageUrl);
        }

        public void GivenIAmOnModelDetailsPage()
        {
            var modelPageUrl = $"{TestContext.BaseUrl}{ModelDetailsPage.UrlPath}";
            var modelIndex = 1;
            if (!ModelDetailsPage.CurrentUrl.Contains(modelPageUrl))
            {
                HomePage
                    .TopNavMenu
                    .ClickBrandLink()
                    .ClickOverallImage()
                    .SelectModel(modelIndex);
            }

            ModelDetailsPage
                .CurrentUrl
                .Should()
                .Contain(modelPageUrl);

            TestContext["SelectedModelIndex"] = modelIndex;
        }

        public void GivenIAmOnMakePage()
        {
            var makePageUrl = $"{TestContext.BaseUrl}{MakePage.UrlPath}";
            if (!MakePage.CurrentUrl.Contains(makePageUrl))
            {
                HomePage
                    .ClickMakeImage();
            }

            MakePage
                .CurrentUrl
                .Should()
                .Contain(makePageUrl);
        }

        public void GivenIAmLoggedInAsUser(string username, string password)
        {
            if (TestContext.LoginUser == null || TestContext.LoginUser.Username != username)
            {
                TestContext.LoginUser = new LoginUser()
                {
                    Username = username,
                    Password = password
                };
            }

            if (!HomePage.TopNavMenu.IsLoggedIn())
            {
                var loginUser = TestContext.LoginUser;
                HomePage.TopNavMenu.Login(loginUser.Username, loginUser.Password);
            }
        }

        [Given(@"I am logged in as (Admin|Business|Internal|Customer) user")]
        public void GivenIAmLoggedinAsUser(UserType userType)
        {
            if (TestContext.LoginUser == null || TestContext.LoginUser.UserType != userType)
            {
                var user =
                TestContext
                .UserCredentials
                .Single(x => x.UserType == userType);

                TestContext.LoginUser = user;
            }

            var loginUser = TestContext.LoginUser;

            if (!HomePage.TopNavMenu.IsLoggedIn(loginUser.FirstName))
            {
                HomePage
                    .TopNavMenu
                    .Login(loginUser.Username, loginUser.Password);
            }

            HomePage
                .TopNavMenu
                .IsLoggedIn(loginUser.FirstName)
                .Should()
                .BeTrue();
        }

        [Given(@"I have registered a new (Admin|Business|Internal|Customer) user")]
        public void GivenIHaveCreatedAUser(UserType userType)
        {
            LoginUser featureLoginUser;

            if (!FeatureContext.ContainsKey("LoginUser"))
            {
                featureLoginUser = UserFactory.Create(userType);

                var registerPage =
                    HomePage
                    .TopNavMenu
                    .ClickRegister();

                registerPage.RegisterUser(featureLoginUser);
                FeatureContext["LoginUser"] = featureLoginUser;
            }
            else
            {
                featureLoginUser = (LoginUser)FeatureContext["LoginUser"];
            }

            TestContext.LoginUser = featureLoginUser;
        }

        [Given(@"I am not logged in")]
        public void GivenIAmNotLoggedIn()
        {
            if (HomePage.TopNavMenu.IsLoggedIn())
            {
                HomePage.TopNavMenu.ClickLogout();
            }

            HomePage
                .TopNavMenu
                .IsLoggedIn()
                .Should()
                .BeFalse();
        }

        [When(@"I login with Username (.*) and Password (.*)")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            HomePage
                .TopNavMenu
                .Login(username, password);
        }



        [Then(@"I should see an error message (.*)")]
        public virtual void ThenIShouldSeeAnErrorMessage(string errorMessage)
        {
            HomePage
                .GetErrorMessage(errorMessage)
                .Should()
                .NotBeNullOrEmpty()
                .And
                .Be(errorMessage);
        }

        [Then(@"I should be redirected to home page")]
        public void ThenIShouldBeRedirectedToHomePage()
        {
            HomePage.CurrentUrl.Should().Be(TestContext.BaseUrl + HomePage.UrlPath);
        }

    }
}

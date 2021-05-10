using TechTalk.SpecFlow;
using TestAutomation.Tests.UI.Pages;
using FluentAssertions;
using TestAutomation.Framework.UI;
using System.Linq;
using System;

namespace TestAutomation.Tests.UI.Steps
{
    [Binding, Scope(Tag = "Model")]
    public class ModelSteps : BaseStep
    {
        public ModelDetailsPage ModelDetailsPage { get; set; }
        public OverallPage OverallPage { get; set; }
        public ModelSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        {
            OverallPage = PageFactory.Create<OverallPage>(Driver, TestContext.DriverTimeout);
            ModelDetailsPage = PageFactory.Create<ModelDetailsPage>(Driver, TestContext.DriverTimeout);
        }

        

        [Given(@"I haven't voted for a model yet")]
        public void GivenIHavenTVotedForAModelYet()
        {
            while (!ModelDetailsPage.IsVoteAvailable())
            {
                var modelIndex = (int)TestContext["SelectedModelIndex"];

                HomePage
                    .TopNavMenu
                    .ClickBrandLink()
                    .ClickOverallImage()
                    .SelectModel(++modelIndex);

                TestContext["SelectedModelIndex"] = modelIndex;
            }
        }


        [When(@"I vote for a model (with|without) comment")]
        public void WhenIVoteForAModelWithComment(string commentAvailability)
        {
            var originalVoteCount = ModelDetailsPage.GetVoteCount();
            var comment = "";

            if (commentAvailability == "with")
            {
                comment = $"{TestContext.LoginUser.Username} - Comment";
            }

            ModelDetailsPage.Vote(comment);

            TestContext["Comment"] = comment;
            TestContext["VoteCount"] = originalVoteCount;
        }

        [Then(@"I should see the thanks message")]
        public void ThenIShouldSeeTheThanksMessage()
        {
            ModelDetailsPage
                .IsVoteAvailable()
                .Should()
                .BeFalse();
        }

        [Then(@"I should see the total vote count is increased")]
        public void ThenIShouldSeeTheTotalVoteCountIsIncreased()
        {
            var previousVoteCount = TestContext["VoteCount"];
            var currentVoteCount = ModelDetailsPage.GetVoteCount();

            if (previousVoteCount == null)
            {
                currentVoteCount.Should().Be(1);
            }
            else
            {
                currentVoteCount.Should().Be((int)previousVoteCount + 1);
            }
        }

        [Then(@"I should see the new comment (is|is not) added")]
        public void ThenIShouldSeeTheNewCommentIsAdded(string commentAvailability)
        {
            var latestComment = ModelDetailsPage.GetComments(1).Single();
            
            if (commentAvailability == "is")
            {
                var expectedComment = (string)TestContext["Comment"];

                latestComment
                    .Date
                    .Should()
                    .BeWithin(TimeSpan.FromSeconds(20));

                latestComment
                    .Context
                    .Should()
                    .Be(expectedComment);
            }
            else
            {
                latestComment
                    .Date
                    .Should()
                    .NotBeAfter(DateTime.Now.AddSeconds(-20));
            }
        }
    }
}
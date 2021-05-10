using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages
{
    public class ModelDetailsPage : BasePage
    {
        protected IWebElement CommentTextfield => FluentFindElement(By.Id("comment"));
        protected IWebElement VoteButton => FluentFindElement(By.CssSelector("button.btn-success"));
        protected IWebElement CarModelTitle => FluentFindElement(By.CssSelector("h3"));
        protected List<IWebElement> CommentRows => FluentFindElements(By.CssSelector("tbody tr"));
        protected IWebElement VoteCount => FluentFindElement(By.CssSelector("div.card-block h4 strong"));
        protected IWebElement VoteSuccessfulMessage => FluentFindElement(By.XPath("//p[text()='Thank you for your vote!']"), false, 5000);
        public ModelDetailsPage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/model";
        }

        public ModelDetailsPage Vote(string comment)
        {
            if (!comment.IsNullOrEmpty())
            {
                CommentTextfield.ClearAndSendKeys(comment);
            }

            VoteButton.Click();
            return this;
        }

        public bool IsVoteAvailable()
        {
            return VoteSuccessfulMessage == null;
        }

        public List<Comment> GetComments(int latestCommentCount)
        {
            List<Comment> comments = new List<Comment>();
            var commentCount = latestCommentCount > CommentRows.Count ? CommentRows.Count : latestCommentCount;

            for (int i = 0; i < commentCount; i++)
            {
                var row = CommentRows[i];
                var columns = row.FindElements(By.CssSelector("td"));
                comments.Add(new Comment
                {
                    Date = DateTime.Parse(columns[0].Text),
                    Author = columns[1].Text,
                    Context = columns[2].Text
                });
            }

            return comments;
        }

        public string GetCarModel()
        {
            return CarModelTitle.Text;
        }

        public int? GetVoteCount()
        {
            var voteText = VoteCount?.Text;
            int? result = null;
            if (voteText != null)
            {
                result = int.Parse(voteText);
            }

            return result;
        }
    }
}
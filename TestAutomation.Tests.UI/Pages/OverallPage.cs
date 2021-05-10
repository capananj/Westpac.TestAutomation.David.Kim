using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Pages
{
    public class OverallPage : BasePage
    {
        protected List<IWebElement> CarTableRows => FluentFindElements(By.CssSelector("table.cars tbody tr"));
        protected IWebElement CarModel(string model) => FluentFindElement(By.XPath($"//td/a[text()='{model}']"));
        protected IWebElement CarModel(int index) => FluentFindElement(By.XPath($"//tr[{index}]//td[3]/a[text()]"));
        public OverallPage(RemoteWebDriver driver, double driverTimeout) : base(driver, driverTimeout)
        {
            UrlPath = "/overall";
        }

        public List<Car> GetCarList()
        {
            var carList = new List<Car>();
            foreach (var row in CarTableRows)
            {
                var columns = row.FindElements(By.CssSelector("td"));
                carList.Add(new Car
                {
                    Make = columns[1].FindElement(By.CssSelector("a")).Text,
                    Model = columns[2].FindElement(By.CssSelector("a")).Text,
                    Rank = int.Parse(columns[3].FindElement(By.CssSelector("a")).Text),
                    Votes = int.Parse(columns[4].FindElement(By.CssSelector("a")).Text),
                    Engine = columns[5].FindElement(By.CssSelector("a")).Text,
                    Comments =
                        columns[6].FindElements(By.CssSelector("div p"))
                        .Select(x => new Comment
                        {
                            Context = x.Text
                        })
                        .ToList()
                });
            }

            return carList;
        }

        public ModelDetailsPage SelectModel(string model)
        {
            CarModel(model).Click();
            return PageFactory.Create<ModelDetailsPage>(Driver, DriverTimeout);
        }

        public ModelDetailsPage SelectModel(int index)
        {
            CarModel(index).Click();
            return PageFactory.Create<ModelDetailsPage>(Driver, DriverTimeout);
        }
    }
}
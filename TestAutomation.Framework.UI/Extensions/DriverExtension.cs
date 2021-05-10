using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestAutomation.Framework.UI
{
    public static class DriverExtension
    {
        public static IWebElement FluentFindElement(this IWebDriver driver, By by, bool failIfNotFound = true, double timeoutMilliseconds = 3000)
        {
            IWebElement element = null;

            try
            {
                var failureMessage = $"Failed to find element [{by}]";
                element
                    = Wait(driver, failureMessage, timeoutMilliseconds)
                    .Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (WebDriverTimeoutException e)
            {
                if (failIfNotFound)
                {
                    throw e;
                }
            }

            return element;
        }

        public static List<IWebElement> FluentFindElements(this IWebDriver driver, By by, bool failIfNotFound = true, double timeoutMilliseconds = 3000)
        {
            List<IWebElement> elements = null;

            try
            {
                var failureMessage = $"Failed to find elements [{by}]";
                var wait = Wait(driver, failureMessage, timeoutMilliseconds);

                elements =
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by))
                    .ToList();
            }
            catch (WebDriverTimeoutException e)
            {
                if (failIfNotFound)
                {
                    throw e;
                }
            }

            return elements;
        }

        public static string ScreenCaptureAsBase64String(this IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }

        public static WebDriverWait Wait(IWebDriver driver, string failureMessage, double timeoutMilliseconds = 5000)
        {
            var pollingInterval =
                timeoutMilliseconds < 5000 ?
                500 :
                timeoutMilliseconds / 10;

            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutMilliseconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval),
                Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds),
                Message = failureMessage
            };

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }

        public static string TakeScreenshotAsBase64(this IWebDriver driver)
        {
            return ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
        }
    }
}

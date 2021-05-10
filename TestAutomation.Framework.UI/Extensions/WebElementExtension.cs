using OpenQA.Selenium;

namespace TestAutomation.Framework.UI
{
    public static class WebElementExtension
    {
        public static void ClearAndSendKeys(this IWebElement element, object value)
        {
            element.Clear();
            element.SendKeys(value.ToString());
        }
    }
}

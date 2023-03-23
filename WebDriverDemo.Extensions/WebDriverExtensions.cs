using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverDemo.Extensions
{
    public static class WebDriverExtensions
    {
        public static void WaitForPageLoad(this IWebDriver webDriver, int timeoutInSeconds = 60)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(d =>
            {
                string readyState = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString();

                return readyState.Equals("complete", StringComparison.OrdinalIgnoreCase);
            });
        }
    }
}
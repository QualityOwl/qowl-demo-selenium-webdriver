using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverDemo.Extensions
{
    public static class WebDriverExtensions
    {
        private const int _defaultTimeoutInterval = 60;

        public static void WaitForPageLoad(this IWebDriver webDriver, int timeoutInSeconds = _defaultTimeoutInterval)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitForElement(this IWebDriver webDriver, By locator, int timeoutInSeconds = _defaultTimeoutInterval)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.PollingInterval = TimeSpan.FromMilliseconds(1000);

            var staleElement = true;

            while (staleElement)
            {
                try
                {
                    wait.Until(driver =>
                    {
                        var element = driver.FindElement(locator);

                        return element.Enabled;
                    });

                    staleElement = false;
                }
                catch (StaleElementReferenceException ex)
                {
                    staleElement = true;
                }
            }
        }
    }
}
using OpenQA.Selenium;
using System;
using System.Threading;
using WebDriverDemo.Extensions;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class GoogleRunner
    {
        public IWebDriver WebDriver { get; private set; }
        public string BaseUrl => "https://www.google.com/";

        public string CurrentUrl
        {
            get
            {
                WebDriver.WaitForPageLoad();

                return WebDriver.Url;
            }
        }

        public GoogleRunner(ITestOutputHelper log, IWebDriver webDriver)
        {
            log.StepDescription($"Navigate to '{BaseUrl}'.");
            WebDriver = webDriver;
            WebDriver.Url = BaseUrl;
            WebDriver.WaitForPageLoad();
        }

        public void WaitForUrlChange(string expectedUrl, int waitTimeSeconds = 10)
        {
            var startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalSeconds <= waitTimeSeconds)
            {
                if (WebDriver.Url.Contains(expectedUrl))
                {
                    break;
                }

                Thread.Sleep(1000); // Wait for 1 second before checking again.
            }
        }

        public Home Home => new Home(WebDriver);

        public Gmail Gmail => new Gmail(WebDriver);
    }
}
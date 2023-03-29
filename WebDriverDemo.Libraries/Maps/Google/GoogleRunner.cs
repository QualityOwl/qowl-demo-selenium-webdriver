using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using WebDriverDemo.Extensions;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class GoogleRunner
    {
        public IWebDriver WebDriver { get; private set; }
        public string BaseUrl => "https://www.google.com/";
        public string CurrentUrl => WebDriver.Url;

        public GoogleRunner(ITestOutputHelper output, IWebDriver webDriver)
        {
            output.StepDescription($"Navigate to '{BaseUrl}'.");
            WebDriver = webDriver;
            WebDriver.Url = BaseUrl;
            WebDriver.WaitForPageLoad();
        }

        public void PressPageDownKey(int numberOfPresses = 1)
        {
            var actions = new Actions(WebDriver);

            for (int i = 1; i <= numberOfPresses; i++)
            {
                actions.SendKeys(Keys.PageDown).Perform();

                actions.Pause(TimeSpan.FromSeconds(3));
            }
        }

        public Search Search => new Search(WebDriver);
    }
}
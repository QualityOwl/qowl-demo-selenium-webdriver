using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;
using WebDriverDemo.Extensions;
using WebDriverDemo.Extensions.Logging;
using WebDriverDemo.Libraries;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests
{
    public class GoogleTests : TestBase
    {
        private string _baseUrl = "https://www.google.com/";

        public GoogleTests(ITestOutputHelper log) : base(log)
        {
            Log.StepDescription($"Initialize webdriver and navigate to '{_baseUrl}'");
            WebDriver = new ChromeDriver()
            {
                Url = _baseUrl
            };
            WebDriver.WaitForPageLoad();
        }

        [Fact]
        [Trait("By", "CssSelector")]
        public void GoogleSearch_SearchForYellowSubmarineWikie_YellowSubmarineWikiHyperlinkIsPresentInResults()
        {
            // Arrange
            var searchTerm = "yellow submarine wiki";

            // Act
            Log.StepDescription("Enter search term into search field.");
            var searchField = WebDriver.FindElement(By.CssSelector("input[type=\"text\"]"));
            searchField.SendKeys(searchTerm);

            Log.StepDescription("Click 'Search' button.");
            ClickSearchButton();

            // Assert
            Log.StepDescription("Validate the returned search results page.");
            var actualResultElement = WebDriver.FindElement(By.CssSelector(@"a[href*=Yellow_Submarine_\(song\)]"));
            Assert.True(actualResultElement.Text.Contains("Yellow Submarine (song)"));
        }

        [Fact]
        [Trait("By", "XPath")]
        public void GoogleSearch_SearchForSaturnFiveRocket_EncyclopediaAstronauticaHyperlinkIsPresentInResults()
        {
            // Arrange
            var searchTerm = "saturn five rocket";

            // Act
            Log.StepDescription("Enter search term into search field.");
            var searchField = WebDriver.FindElement(By.XPath("//input[@type='text']"));
            searchField.SendKeys(searchTerm);

            Log.StepDescription("Click 'Search' button.");
            ClickSearchButton();

            Log.StepDescription("Scroll down to the fifth results page.");
            PressPageDownKey(5);

            // Assert
            Log.StepDescription("Validate the returned search results page.");
            var actualResultElement = WebDriver.FindElement(By.XPath(@"//a[@href='http://www.astronautix.com/s/saturnv.html']"));
            Assert.True(actualResultElement.Text.Contains("Saturn V"));
        }

        private void ClickSearchButton()
        {
            var searchButtonElements = WebDriver.FindElements(By.XPath("//input[@type='submit'][@value='Google Search']")).ToList();

            foreach (var button in searchButtonElements)
            {
                var clientWidth = (long)((IJavaScriptExecutor)WebDriver).ExecuteScript("return arguments[0].clientWidth;", button);

                if (clientWidth > 0)
                {
                    button.Click();
                    break;
                }
            }
        }

        private void PressPageDownKey(int numberOfPresses = 1)
        {
            var actions = new Actions(WebDriver);

            for (int i = 1; i <= numberOfPresses; i++)
            {
                actions.SendKeys(Keys.PageDown).Perform();

                actions.Pause(TimeSpan.FromSeconds(3));
            }
        }
    }
}
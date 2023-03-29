using OpenQA.Selenium.Interactions;
using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries;
using WebDriverDemo.Libraries.Maps.Google;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests
{
    public class GoogleTests : TestBase
    {
        private GoogleRunner _googleRunner;

        public GoogleTests(ITestOutputHelper output) : base(output)
        {
            Log.StepDescription("Instantiate Google test runner.");
            _googleRunner = new GoogleRunner(output, WebDriver);
        }

        [Fact]
        public void GoogleSearch_SearchForYellowSubmarineWiki_YellowSubmarineWikiPageIsDisplayed()
        {
            // Arrange
            var searchTerm = "yellow submarine wiki";
            var expectedLinkText = "Yellow Submarine (song)";
            var expectedUrl = "https://en.wikipedia.org/wiki/Yellow_Submarine_(song)";

            // Act
            Log.StepDescription($"Enter '{searchTerm}' into 'Search' field.");
            _googleRunner.Search.EnterSearchTextbox(searchTerm);

            Log.StepDescription("Click 'Search' button.");
            _googleRunner.Search.ClickSearchButton();

            Log.StepDescription($"Click '{expectedLinkText}' hyperlink.");
            _googleRunner.Search.ClickHyperlink(expectedLinkText);

            // Assert
            Log.StepDescription($"Verify that the '{expectedLinkText}' page successfully displays");
            var actualUrl = _googleRunner.CurrentUrl;
            Assert.Equal(expectedUrl, actualUrl);
        }
    }
}
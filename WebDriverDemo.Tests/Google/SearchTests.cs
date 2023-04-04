using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Core;
using WebDriverDemo.Libraries.Maps.Google;
using WebDriverDemo.Libraries.Wrappers;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests.Google
{
    public class SearchTests : TestBase
    {
        private GoogleRunner _googleRunner;

        public SearchTests(ITestOutputHelper output) : base(output)
        {
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
            _googleRunner.Home.EnterSearchTextbox(searchTerm);

            Log.StepDescription("Click 'Search' button.");
            _googleRunner.Home.ClickSearchButton();

            Log.StepDescription($"Click '{expectedLinkText}' hyperlink.");
            _googleRunner.Home.ClickHyperlink(expectedLinkText);

            // Assert
            Log.StepDescription($"Verify that the '{expectedLinkText}' page successfully displays");
            var actualUrl = _googleRunner.CurrentUrl;
            AssertWrapper.Equal(expectedUrl, actualUrl);            
        }
    }
}
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
        private GoogleRunner _google;

        public SearchTests(ITestOutputHelper output) : base(output)
        {
            _google = new GoogleRunner(output, WebDriver);
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
            _google.Home.EnterSearchTextbox(searchTerm);

            Log.StepDescription("Click 'Search' button.");
            _google.Home.ClickSearchButton();

            Log.StepDescription($"Click '{expectedLinkText}' hyperlink.");
            _google.Home.ClickHyperlink(expectedLinkText);

            // Assert
            Log.StepDescription($"Verify that the '{expectedLinkText}' page successfully displays");
            var actualUrl = _google.CurrentUrl;
            AssertWrapper.Equal(expectedUrl, actualUrl);            
        }
    }
}
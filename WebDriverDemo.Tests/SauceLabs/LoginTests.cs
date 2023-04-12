using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Core;
using WebDriverDemo.Libraries.Maps.SauceLabs;
using WebDriverDemo.Libraries.Wrappers;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests.SauceLabs
{
    public class LoginTests : TestBase
    {
        private SauceLabsRunner _sauceLabs;

        public LoginTests(ITestOutputHelper output) : base(output)
        {
            _sauceLabs = new SauceLabsRunner(output, WebDriver);
        }

        [Fact]
        [Trait("Priority", "High")]
        public void Login_LogInWithStandardUser_LoginIsSuccessful()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription("Validate successful login.");
            _sauceLabs.Inventory.AssertLocation();
        }

        [Fact]
        [Trait("Priority", "High")]
        public void Login_LogInWithLockedOutUser_LoginErrorMessageTextIsDisplayed()
        {
            // Arrange
            var username = "locked_out_user";
            var password = "secret_sauce";
            var expectedErrorText = "Epic sadface: Sorry, this user has been locked out.";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription("Validate login error message is correct.");
            var actualErrorText = _sauceLabs.Login.GetErrorMessageText();
            AssertWrapper.Equal(expectedErrorText, actualErrorText);
        }
    }
}
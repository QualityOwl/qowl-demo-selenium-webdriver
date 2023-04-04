using WebDriverDemo.Security;
using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Core;
using WebDriverDemo.Libraries.Maps.Google;
using WebDriverDemo.Libraries.Wrappers;
using Xunit;
using Xunit.Abstractions;
using System.Threading;

namespace WebDriverDemo.Tests.Google
{
    public class GmailTests : TestBase
    {
        private GoogleRunner _googleRunner;
        private Credentials _credentials;

        public GmailTests(ITestOutputHelper output) : base(output)
        {
            _googleRunner = new GoogleRunner(output, WebDriver);
            _credentials = new Credentials();
        }

        [Fact]
        [Trait("TestCategory", "Smoke")]
        [Trait("TestCategory", "Day1")]
        public void GoogleMail_LogIntoAccount_AccountLoginIsSuccessful()
        {
            // Arrange
            var emailAddress = _credentials.SecretsObject["google"]["emailAddress"].ToString();
            var password = _credentials.SecretsObject["google"]["password"].ToString();
            var expectedUrl = "https://mail.google.com/mail/u/0/#inbox";

            // Act
            Log.StepDescription($"Navigate to Gmail login page.");
            _googleRunner.Home.ExpandAppsMenu();

            Log.StepDescription("Click 'Gmail' button.");
            _googleRunner.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _googleRunner.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _googleRunner.Gmail.Signin.EnterEmailAddress(emailAddress);
            _googleRunner.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _googleRunner.Gmail.Signin.EnterPassword(password);
            _googleRunner.Gmail.Signin.ClickNextButton();

            // Assert            
            Log.StepDescription("Validate successful login.");
            _googleRunner.WaitForUrlChange(expectedUrl);
            
            var actualURL = _googleRunner.CurrentUrl;
            AssertWrapper.Equal(expectedUrl, actualURL);
        }

        [Fact]
        [Trait("TestCategory", "Regression")]
        [Trait("TestCategory","Day1")]
        public void GoogleMail_SendEmail_EmailSendIsSuccessful()
        {
            // Arrange
            var emailAddress = _credentials.SecretsObject["google"]["emailAddress"].ToString();
            var password = _credentials.SecretsObject["google"]["password"].ToString();
            var toEmailAddress = "bluemustardtest+abc@gmail.com";
            var subject = "This is a test - ABC123"; //.AppendRandomCharacters(5);
            var body = "Hello, world!";
            
            // Act
            Log.StepDescription($"Navigate to Gmail login page.");
            _googleRunner.Home.ExpandAppsMenu();
            
            Log.StepDescription("Click 'Gmail' button.");
            _googleRunner.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _googleRunner.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _googleRunner.Gmail.Signin.EnterEmailAddress(emailAddress);
            _googleRunner.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _googleRunner.Gmail.Signin.EnterPassword(password);
            _googleRunner.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Click 'Compose' button.");
            _googleRunner.Gmail.Inbox.ClickComposeButton();

            Log.StepDescription("Complete 'New Message' window fields.");
            _googleRunner.Gmail.Inbox.EnterNewMessage(toEmailAddress, subject, body);

            Log.StepDescription("Click 'Send' button.");
            _googleRunner.Gmail.Inbox.ClickSendButton();
            Thread.Sleep(3000);

            // Assert            
            Log.StepDescription("Verify email was successfully sent.");
            AssertWrapper.True(_googleRunner.Gmail.Inbox.IsEmailDisplayed(subject));
        }

        [Fact]
        [Trait("TestCategory", "Regression")]
        [Trait("TestCategory", "Day2")]
        public void GoogleMail_DeleteEmail_EmailSendIsSuccessful()
        {
            // Arrange
            var emailAddress = _credentials.SecretsObject["google"]["emailAddress"].ToString();
            var password = _credentials.SecretsObject["google"]["password"].ToString();
            var subject = "This is a test - ABC123"; //.AppendRandomCharacters(5);

            // Act
            Log.StepDescription($"Navigate to Gmail login page.");
            _googleRunner.Home.ExpandAppsMenu();

            Log.StepDescription("Click 'Gmail' button.");
            _googleRunner.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _googleRunner.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _googleRunner.Gmail.Signin.EnterEmailAddress(emailAddress);
            _googleRunner.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _googleRunner.Gmail.Signin.EnterPassword(password);
            _googleRunner.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Click 'Send' button.");
            _googleRunner.Gmail.Inbox.DeleteEmail(subject);

            // Assert            
            Log.StepDescription("Verify email was successfully sent.");
            AssertWrapper.False(_googleRunner.Gmail.Inbox.IsEmailDisplayed(subject));
        }
    }
}
using System.Threading;
using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Core;
using WebDriverDemo.Libraries.Runners;
using WebDriverDemo.Libraries.Wrappers;
using WebDriverDemo.Security;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests.Google
{
    public class GmailTests : TestBase
    {
        private GoogleRunner _google;
        private Credentials _credentials;

        public GmailTests(ITestOutputHelper log) : base(log)
        {
            _google = new GoogleRunner(Log, WebDriver);
            _credentials = new Credentials();
        }

        [Fact]
        [Trait("Priority", "High")]
        [Trait("Sequence", "Day1")]
        public void GoogleMail_LogIntoAccount_AccountLoginIsSuccessful()
        {
            // Arrange
            var emailAddress = _credentials.SecretsObject["google"]["emailAddress"].ToString();
            var password = _credentials.SecretsObject["google"]["password"].ToString();
            var expectedUrl = "https://mail.google.com/mail/u/0/#inbox";

            // Act
            Log.StepDescription($"Navigate to Gmail login page.");
            _google.Home.ExpandAppsMenu();

            Log.StepDescription("Click 'Gmail' button.");
            _google.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _google.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _google.Gmail.Signin.EnterEmailAddress(emailAddress);
            _google.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _google.Gmail.Signin.EnterPassword(password);
            _google.Gmail.Signin.ClickNextButton();

            // Assert
            Log.StepDescription("Validate successful login.");
            _google.WaitForUrlChange(expectedUrl);

            var actualURL = _google.CurrentUrl;
            AssertWrapper.Equal(expectedUrl, actualURL);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        [Trait("Sequence", "Day1")]
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
            _google.Home.ExpandAppsMenu();

            Log.StepDescription("Click 'Gmail' button.");
            _google.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _google.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _google.Gmail.Signin.EnterEmailAddress(emailAddress);
            _google.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _google.Gmail.Signin.EnterPassword(password);
            _google.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Click 'Compose' button.");
            _google.Gmail.Inbox.ClickComposeButton();

            Log.StepDescription("Complete 'New Message' window fields.");
            _google.Gmail.Inbox.EnterNewMessage(toEmailAddress, subject, body);

            Log.StepDescription("Click 'Send' button.");
            _google.Gmail.Inbox.ClickSendButton();
            Thread.Sleep(3000);

            // Assert
            Log.StepDescription("Verify email was successfully sent.");
            AssertWrapper.True(_google.Gmail.Inbox.IsEmailDisplayed(subject));
        }

        [Fact]
        [Trait("Priority", "Regression")]
        [Trait("Sequence", "Day2")]
        public void GoogleMail_DeleteEmail_EmailSendIsSuccessful()
        {
            // Arrange
            var emailAddress = _credentials.SecretsObject["google"]["emailAddress"].ToString();
            var password = _credentials.SecretsObject["google"]["password"].ToString();
            var subject = "This is a test - ABC123"; //.AppendRandomCharacters(5);

            // Act
            Log.StepDescription($"Navigate to Gmail login page.");
            _google.Home.ExpandAppsMenu();

            Log.StepDescription("Click 'Gmail' button.");
            _google.Home.ClickAppMenuItem("Gmail");

            Log.StepDescription("Click 'Signin' button.");
            _google.Gmail.About.ClickSigninButton();

            Log.StepDescription("Enter 'Email address'.");
            _google.Gmail.Signin.EnterEmailAddress(emailAddress);
            _google.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Enter 'Password'.");
            _google.Gmail.Signin.EnterPassword(password);
            _google.Gmail.Signin.ClickNextButton();

            Log.StepDescription("Click 'Send' button.");
            _google.Gmail.Inbox.DeleteEmail(subject);

            // Assert
            Log.StepDescription("Verify email was successfully sent.");
            AssertWrapper.False(_google.Gmail.Inbox.IsEmailDisplayed(subject));
        }
    }
}
using OpenQA.Selenium;

namespace WebDriverDemo.Libraries.Maps.SauceLabs
{
    public class Login : MapBase
    {
        private readonly IWebDriver _webDriver;

        private string _absoluteUrl = "https://www.saucedemo.com/";

        public Login(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void EnterUsername(string username)
        {
            UIUsernameEdit.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            UIPasswordEdit.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            UILoginButton.Click();
        }

        public string GetErrorMessageText()
        {
            return UIErrorMessageContainer.Text;
        }

        private IWebElement UIUsernameEdit => _webDriver.FindElement(By.CssSelector("input#user-name"));

        private IWebElement UIPasswordEdit => _webDriver.FindElement(By.CssSelector("input#password"));

        private IWebElement UILoginButton => _webDriver.FindElement(By.CssSelector("input#login-button"));

        private IWebElement UIErrorMessageContainer => _webDriver.FindElement(By.CssSelector("div.error-message-container.error"));
    }
}
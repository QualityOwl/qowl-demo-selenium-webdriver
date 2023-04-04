using OpenQA.Selenium;
using System.Threading;
using WebDriverDemo.Extensions;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class Signin
    {
        private readonly IWebDriver _webDriver;

        public Signin(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void EnterEmailAddress(string emailAddress)
        {
            _webDriver.WaitForPageLoad();

            var textbox = _webDriver.FindElement(By.CssSelector("input[type='email']"));

            textbox.SendKeys(emailAddress);
        }

        public void EnterPassword(string password)
        {
            Thread.Sleep(5000);

            _webDriver.WaitForPageLoad();

            var textbox = _webDriver.FindElement(By.CssSelector("input[type='password']"));

            textbox.SendKeys(password);
        }

        public void ClickNextButton()
        {
            _webDriver.WaitForPageLoad();

            var button = _webDriver.FindElement(By.XPath("//span[contains(text(), 'Next')]"));

            button.Click();
        }
    }
}
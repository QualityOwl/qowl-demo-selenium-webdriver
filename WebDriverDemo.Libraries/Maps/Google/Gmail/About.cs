using OpenQA.Selenium;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class About
    {
        private readonly IWebDriver _webDriver;

        public About(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ClickSigninButton()
        {
            var button = _webDriver.FindElement(By.CssSelector("a.button[data-action='sign in']"));

            button.Click();
        }


    }
}
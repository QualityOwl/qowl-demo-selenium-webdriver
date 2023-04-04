using OpenQA.Selenium;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class Gmail
    {
        private readonly IWebDriver _webDriver;

        public Gmail(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public About About => new About(_webDriver);

        public Signin Signin => new Signin(_webDriver);

        public Inbox Inbox => new Inbox(_webDriver);
    }
}
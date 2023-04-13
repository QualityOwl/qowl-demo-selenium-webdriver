using OpenQA.Selenium;

namespace WebDriverDemo.Libraries.Runners
{
    public abstract class RunnerBase
    {
        private IWebDriver _webDriver;

        public RunnerBase(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
    }
}
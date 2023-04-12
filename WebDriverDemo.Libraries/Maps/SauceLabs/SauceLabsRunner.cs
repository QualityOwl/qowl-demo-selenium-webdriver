using OpenQA.Selenium;
using WebDriverDemo.Extensions;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries.Maps.SauceLabs
{
    public class SauceLabsRunner : RunnerBase
    {
        public IWebDriver WebDriver { get; private set; }
        public string BaseUrl => "https://www.saucedemo.com/";

        public string CurrentUrl
        {
            get
            {
                WebDriver.WaitForPageLoad();

                return WebDriver.Url;
            }
        }

        public SauceLabsRunner(ITestOutputHelper log, IWebDriver webDriver) : base(webDriver)
        {
            log.StepDescription($"Navigate to '{BaseUrl}'.");
            WebDriver = webDriver;
            WebDriver.Url = BaseUrl;
            WebDriver.WaitForPageLoad();
        }

        public Login Login => new Login(WebDriver);

        public Inventory Inventory => new Inventory(WebDriver);
    }
}
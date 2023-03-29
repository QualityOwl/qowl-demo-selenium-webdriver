using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverDemo.Extensions;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries
{
    public abstract class TestBase : IDisposable
    {
        protected ITestOutputHelper Log;
        protected IWebDriver WebDriver;

        public TestBase(ITestOutputHelper log)
        {
            Log = log;
            Log.HeaderText();

            WebDriver = new ChromeDriver();
        }

        public void Dispose()
        {
            Log.FooterText();
            WebDriver.Quit();
        }
    }
}
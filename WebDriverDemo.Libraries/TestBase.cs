using OpenQA.Selenium;
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
        }

        public void Dispose()
        {
            WebDriver.Quit();

            Log.FooterText();
        }
    }
}
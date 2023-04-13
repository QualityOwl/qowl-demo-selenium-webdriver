using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Wrappers;
using Xunit.Abstractions;

namespace WebDriverDemo.Libraries.Core
{
    public abstract class TestBase : IDisposable
    {
        protected ITestOutputHelper Log;
        protected IWebDriver WebDriver;

        public TestBase(ITestOutputHelper log)
        {
            Log = log;
            Log.HeaderText();
            Cleanup();
            SetupObjects();
        }

        private void Cleanup()
        {
            KillProcesses();
            KillBrowsers();
        }

        private void KillProcesses()
        {
            var chromeProcesses = Process.GetProcessesByName("chromedriver");

            foreach (var process in chromeProcesses)
            {
                process.Kill();
            }
        }

        private void KillBrowsers()
        {
            var chromeBrowsers = Process.GetProcessesByName("chrome");

            foreach (var browser in chromeBrowsers)
            {
                browser.Kill();
            }
        }

        private void SetupObjects()
        {
            WebDriver = new ChromeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            AssertWrapper.SetOutputHelper(Log);
        }

        public void Dispose()
        {
            WebDriver.Quit();
            Log.FooterText();
        }
    }
}
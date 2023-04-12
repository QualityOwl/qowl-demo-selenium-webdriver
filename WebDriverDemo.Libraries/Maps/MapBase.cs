using OpenQA.Selenium;
using System;
using System.Threading;
using Xunit;

namespace WebDriverDemo.Libraries.Maps
{
    public abstract class MapBase
    {
        private IWebDriver _webDriver;
        public string AbsoluteUrl { get; set; }

        public MapBase(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void AssertLocation()
        {
            Assert.True(_webDriver.Url.Contains(AbsoluteUrl));
        }

        public void WaitForUrlChange(int waitTimeSeconds = 10)
        {
            var startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalSeconds <= waitTimeSeconds)
            {
                if (_webDriver.Url.Contains(AbsoluteUrl))
                {
                    break;
                }

                Thread.Sleep(1000); // Wait for 1 second before checking again.
            }
        }
    }
}
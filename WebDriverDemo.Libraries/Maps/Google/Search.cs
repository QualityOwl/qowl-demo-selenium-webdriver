﻿using OpenQA.Selenium;
using System.Linq;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class Search
    {
        private readonly IWebDriver _webDriver;

        public Search(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void EnterSearchTextbox(string searchTerm)
        {
            var textbox = _webDriver.FindElement(By.XPath("//*[@title='Search']"));

            textbox.SendKeys(searchTerm);
        }

        public void ClickSearchButton()
        {
            var searchButtonElements = _webDriver.FindElements(By.XPath("//input[@type='submit'][@value='Google Search']")).ToList();

            foreach (var button in searchButtonElements)
            {
                var clientWidth = (long)((IJavaScriptExecutor)_webDriver).ExecuteScript("return arguments[0].clientWidth;", button);

                if (clientWidth > 0)
                {
                    button.Click();
                    break;
                }
            }
        }

        public void ClickHyperlink(string linkText)
        {
            var hyperlink = _webDriver.FindElement(By.XPath($"//*[contains(text(), '{linkText}')]"));

            hyperlink.Click();
        }
    }
}
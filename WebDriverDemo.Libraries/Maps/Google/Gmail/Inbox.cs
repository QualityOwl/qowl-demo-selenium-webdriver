using OpenQA.Selenium;
using System;
using WebDriverDemo.Extensions;

namespace WebDriverDemo.Libraries.Maps.Google
{
    public class Inbox
    {
        private readonly IWebDriver _webDriver;
        private IWebElement _emailRow;

        public Inbox(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement EmailRow(string subject)
        {
            if (_emailRow == null)
            {
                var locator = By.XPath($"//span[contains(text(), '{subject}') and not(descendant::span[contains(text(), '{subject}')])]");

                _webDriver.WaitForElement(locator);

                var subjectLine = _webDriver.FindElement(locator);

                _emailRow = subjectLine.FindElement(By.XPath(".//ancestor::tr[role='row']"));
            }

            return _emailRow;
        }

        public void ClickComposeButton()
        {
            var button = _webDriver.FindElement(By.XPath("//div[contains(text(), 'Compose')]"));

            button.Click();
        }

        public void EnterNewMessage(string toEmailAddress, string subject, string body)
        {
            UIToEmailAddressTextbox().SendKeys(toEmailAddress);

            UISubjectTextbox.SendKeys(subject);

            UIBodyTextbox.SendKeys(body);
        }

        public void ClickSendButton()
        {
            var button = _webDriver.FindElement(By.CssSelector("div[role='button'][aria-label^='Send']"));

            button.Click();
        }

        public void DeleteEmail(string subject)
        {
            if (IsEmailDisplayed(subject))
            {
                ClickEmail(subject);
                ClickDeleteButton();

                if (IsEmailDisplayed(subject))
                {
                    throw new Exception($"Failed to delete email with subject '{subject}'.");
                }
            }
            else
            {
                throw new Exception($"Email with subject '{subject}' was not found to delete.");
            }
        }

        public bool IsEmailDisplayed(string subject)
        {
            return EmailRow(subject).Enabled;
        }

        public void ClickEmail(string subject)
        {
            EmailRow(subject).Click();
        }

        public void ClickEmailCheckbox(string subject)
        {
            var checkbox = EmailRow(subject).FindElement(By.XPath(".//ancestor::div[role='checkbox'][1]"));

            checkbox.Click();
        }

        public void ClickDeleteButton()
        {
            var button = _webDriver.FindElement(By.CssSelector("div[role='button'][aria-label='Delete']"));

            button.Click();
        }

        private IWebElement UINewMessageWindow => _webDriver.FindElement(By.CssSelector("div[role='region'][aria-label='New Message']"));

        private IWebElement UIToEmailAddressTextbox()
        {
            var parent = UINewMessageWindow.FindElement(By.CssSelector("div[aria-label='Search Field']"));

            return parent.FindElement(By.XPath(".//input[@type='text']"));
        }

        private IWebElement UISubjectTextbox => _webDriver.FindElement(By.CssSelector("input[aria-label='Subject']"));

        private IWebElement UIBodyTextbox => _webDriver.FindElement(By.CssSelector("div[aria-label='Message Body']"));
    }
}
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using WebDriverDemo.Libraries.Maps.SauceLabs.Models;

namespace WebDriverDemo.Libraries.Maps.SauceLabs
{
    public class Inventory : MapBase
    {
        private readonly IWebDriver _webDriver;

        private string _absoluteUrl = "/inventory.html";

        public Inventory(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
            AbsoluteUrl = _absoluteUrl;
        }

        /// <summary>
        /// Get a listing object based on innerText that is contained anywhere within the listing element on the webpage.
        /// </summary>
        /// <param name="innerText"></param>
        /// <returns></returns>
        public Listing GetListingObject(string innerText)
        {
            var inventoryItem = GetListingElementByText(innerText);

            try
            {
                return new Listing(inventoryItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private IWebElement GetListingElementByText(string innerText)
        {
            var inventoryList = GetListOfDisplayedInventoryItems();

            foreach (var item in inventoryList)
            {
                var currentInnerText = item.GetAttribute("innerText");

                if (currentInnerText.Contains(innerText))
                {
                    return item;
                }
            }

            throw new Exception($"Inventory item was not found based on '{innerText}' text.");
        }

        private List<IWebElement> GetListOfDisplayedInventoryItems()
        {
            return UIInventoryItemList;
        }

        private List<IWebElement> UIInventoryItemList => _webDriver.FindElements(By.CssSelector("div.inventory_item")).ToList();
    }
}
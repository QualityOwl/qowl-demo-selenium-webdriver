using OpenQA.Selenium;

namespace WebDriverDemo.Libraries.Maps.SauceLabs.Models
{
    public class Listing
    {
        public string Description => _inventoryItemElement.FindElement(By.CssSelector("div.inventory_item_desc")).Text;

        public IWebElement Image => _inventoryItemElement.FindElement(By.CssSelector("img.inventory_item_img"));

        public string Name => _inventoryItemElement.FindElement(By.CssSelector("div.inventory_item_name")).Text;

        public string Price => _inventoryItemElement.FindElement(By.CssSelector("div.inventory_item_price")).Text;

        public IWebElement AddToCartButton => _inventoryItemElement.FindElement(By.CssSelector("button[class$='btn_inventory']"));

        private IWebElement _inventoryItemElement;

        public Listing(IWebElement InventoryItemElement)
        {
            _inventoryItemElement = InventoryItemElement;
        }
    }
}
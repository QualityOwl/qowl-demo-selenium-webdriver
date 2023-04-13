using OpenQA.Selenium;
using WebDriverDemo.Extensions;
using WebDriverDemo.Libraries.Core;
using WebDriverDemo.Libraries.Runners;
using WebDriverDemo.Libraries.Wrappers;
using Xunit;
using Xunit.Abstractions;

namespace WebDriverDemo.Tests.SauceLabs
{
    public class InventoryTests : TestBase
    {
        private SauceLabsRunner _sauceLabs;

        public InventoryTests(ITestOutputHelper log) : base(log)
        {
            _sauceLabs = new SauceLabsRunner(Log, WebDriver);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_GetListingImage_ListingImageIsCorrect()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Backpack";
            var expectedSource = "https://www.saucedemo.com/static/media/sauce-backpack-1200x1500.0a0b85a3.jpg";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription("Validate correct listing image is displayed.");
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            var actualSource = inventoryItem.Image.GetAttribute("src");

            AssertWrapper.Equal(expectedSource, actualSource);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_GetListingImage_ListingImageIsIncorrect()
        {
            // Arrange
            var username = "problem_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Backpack";
            var expectedSource = "https://www.saucedemo.com/static/media/sauce-backpack-1200x1500.0a0b85a3.jpg";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription("Validate incorrect listing image is displayed.");
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            var actualSource = inventoryItem.Image.GetAttribute("src");

            AssertWrapper.NotEqual(expectedSource, actualSource);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_GetListingPrice_ListingPriceIsCorrect()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Fleece Jacket";
            var expectedPrice = "$49.99";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription($"Validate correct price is displayed for the '{listingName}' listing.");
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            var actualPrice = inventoryItem.Price;

            AssertWrapper.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_GetListingDescription_ListingDescriptionIsCorrect()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Onesie";
            var expectedDescription = "Rib snap infant onesie for the junior automation engineer in development. Reinforced 3-snap bottom closure, two-needle hemmed sleeved and bottom won't unravel.";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            // Assert
            Log.StepDescription($"Validate correct description is displayed for the '{listingName}' listing.");
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            var actualDescription = inventoryItem.Description;

            AssertWrapper.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_ClickAddToCartButton_ButtonLabelDisplaysAsRemove()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Bike Light";
            var expectedButtonLabel = "Remove";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            Log.StepDescription("Click 'Add to cart' button.");
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            var buttonElement = inventoryItem.AddToCartButton;
            buttonElement.Click();

            // Assert
            Log.StepDescription($"Validate that the 'Add to cart' button label displays as 'Remove'.");
            inventoryItem = _sauceLabs.Inventory.GetListingObject(listingName);
            buttonElement = inventoryItem.AddToCartButton;
            var actualButtonLabel = buttonElement.Text;
            AssertWrapper.Equal(expectedButtonLabel, actualButtonLabel);
        }

        [Fact]
        [Trait("Priority", "Medium")]
        public void Inventory_ClickRemoveButton_ButtonLabelDisplaysAsAddToCart()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";
            var listingName = "Sauce Labs Backpack";
            var expectedButtonLabel = "Add to cart";

            // Act
            Log.StepDescription("Enter username.");
            _sauceLabs.Login.EnterUsername(username);

            Log.StepDescription("Enter password.");
            _sauceLabs.Login.EnterPassword(password);

            Log.StepDescription("Click 'Login' button.");
            _sauceLabs.Login.ClickLoginButton();

            Log.StepDescription("Click 'Add to cart' button.");
            var buttonElement = GetListingAddToCartButton(listingName);
            buttonElement.Click();

            Log.StepDescription("Click 'Remove' button.");
            buttonElement = GetListingAddToCartButton(listingName);
            buttonElement.Click();

            // Assert
            Log.StepDescription($"Validate that the 'Remove' button label displays as 'Add to cart'.");
            buttonElement = GetListingAddToCartButton(listingName);
            var actualButtonLabel = buttonElement.Text;
            AssertWrapper.Equal(expectedButtonLabel, actualButtonLabel);
        }

        private IWebElement GetListingAddToCartButton(string innerText)
        {
            var inventoryItem = _sauceLabs.Inventory.GetListingObject(innerText);
            return inventoryItem.AddToCartButton;
        }
    }
}
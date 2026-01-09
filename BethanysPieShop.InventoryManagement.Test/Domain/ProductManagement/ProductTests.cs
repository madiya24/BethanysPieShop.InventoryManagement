using System;
using BethanysPieShop.InventoryManagement.ProductManagement;
using Xunit;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Test
{

    public class ProductTests
    {
        [Fact]
        public void UseProduct_Reduces_AmountInStock()
        {
            // Arrange
            RegularProduct product = new RegularProduct(1, "Sugar", "Lorem ispsum", new Price() { ItemPrice = 10, Currency = Currency.Euro }, UnitTypes.PerKg, 100);
            product.IncreaseStock(100); // increase stock by 100

            // Act
            product.UseProduct(20); // use 20 items from stock

            // Assert
            Assert.Equal(80, product.AmountInStock);
        }

        [Fact]
        public void UseProduct_ItemHigherThanStock_NoChnagetoStock()
        {
            // Arrange
            RegularProduct product = new RegularProduct(1, "Sugar", "Lorem ispsum", new Price() { ItemPrice = 10, Currency = Currency.Euro }, UnitTypes.PerKg, 100);
            product.IncreaseStock(10); // increase stock by 10

            // Act
            product.UseProduct(100); // try to use 100 items from stock

            // Assert
            Assert.Equal(10, product.AmountInStock); // stock should remain unchanged
        }

        [Fact]
        public void UseProduct_Reduces_AmountInStock_StockBelowThreshold()
        {
            // Arrange
           RegularProduct product = new RegularProduct(1, "Sugar", "Lorem ispsum", new Price() { ItemPrice = 10, Currency = Currency.Euro }, UnitTypes.PerKg, 100);
            int increaseValue = 100;
            product.IncreaseStock(increaseValue); // increase stock by 100

            // Act
            product.UseProduct(increaseValue - 1); // use items to leave stock just below reorder threshold

            // Assert
            Assert.True(product.IsBelowStockThreshold); // check if stock is below reorder threshold
        }
    }
}

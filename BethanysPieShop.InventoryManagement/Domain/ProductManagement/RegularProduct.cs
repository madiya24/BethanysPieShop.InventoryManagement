using System;
using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class RegularProduct : Product, ISavable
{
    public RegularProduct(int id, string name, string? description, Price price, UnitTypes unitType, int maxAmountInStock) : base(id, name, description, price, unitType, maxAmountInStock)
    {
    }

    public override void IncreaseStock()
    {
            AmountInStock++;
    }
    public string ConvertToStringForSavin()
        {
            return $"{Id}; {Name}; {Description}; {maxItemsInStock};{Price.ItemPrice}; {(int) Price.Currency}; {(int) UnitType};4; ";
        }
}

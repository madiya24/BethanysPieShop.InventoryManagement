using System;
using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class BulkProduct : Product, ISavable
{
    public BulkProduct(int id, string name, string? description, Price price, int maxAmountInStock) : base(id, name, description, price, UnitTypes.PerKg, maxAmountInStock)
    {
    }
    public override void IncreaseStock()
    {
        AmountInStock++;
    }

    public string ConvertToStringForSavin()
    {
        return $"{Id}; {Name}; {Description}; {maxItemsInStock};{Price.ItemPrice}; {(int)Price.Currency}; {(int)UnitType};3;";
    }

    public override object Clone()
        {
            return new BulkProduct(0, this.Name, this.Description, new Price(){ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency}, this.maxItemsInStock);
        }
}

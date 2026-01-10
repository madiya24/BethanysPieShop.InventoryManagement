using System;
using System.Text;
using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class FreshProduct : Product, ISavable
{
    public DateTime ExpiryDate { get; set; }
    public string StorageInstructions { get; set; }

    public FreshProduct(int id, string name, string? description, Price price, UnitTypes unitTypes, int maxAmountInStock) : base(id, name, description, price, unitTypes, maxAmountInStock)
    {

    }

    public override string DisplayDetailsFull()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Id: {Name}\n {Description}\n {Price}\n {AmountInStock} itenm(s) in stock");

        if (IsBelowStockThreshold)
        {
            sb.Append("\n*!!STOCK LOW!!");
        }

        sb.Append("Storage Instructions: " + StorageInstructions);
        sb.Append("Expiry Date: " + ExpiryDate.ToShortDateString());

        return sb.ToString();
    }

    public override void IncreaseStock()
    {
        AmountInStock++;
    }

    public string ConvertToStringForSavin()
    {
        return $"{Id}; {Name}; {Description}; {maxItemsInStock};{Price.ItemPrice}; {(int)Price.Currency}; {(int)UnitType};2; ";
    }

    public override object Clone()
        {
            return new FreshProduct(0, this.Name, this.Description, new Price(){ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency},this.UnitType, this.maxItemsInStock);
        }

}

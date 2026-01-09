using System;
using System.Text;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class FreshProduct: Product
{
    public DateTime ExpiryDate { get; set; }
    public string StorageInstructions { get; set; }

    public FreshProduct(int id, string name, string? description, Price price,UnitTypes unitTypes, int maxAmountInStock) : base(id, name, description, price, unitTypes, maxAmountInStock)
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
       
       sb.Append("Storage Instructions: " + StorageInstructions );
       sb.Append("Expiry Date: " + ExpiryDate.ToShortDateString());

        return sb.ToString();
    }


}

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


}

using System;
using System.Text;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class FreshBoxedProduct: BoxedProduct
{
    public FreshBoxedProduct(int id, string name, string? description, Price price,UnitTypes unitTypes, int maxAmountInStock, int amountPerBox) : base(id, name, description, price, amountPerBox, maxAmountInStock)
    {
        
    }

    
}

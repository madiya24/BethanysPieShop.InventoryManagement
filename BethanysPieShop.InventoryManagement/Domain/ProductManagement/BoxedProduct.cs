using System;
using System.Text;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement;

public class BoxedProduct : Product
{
    private int amountPerBox ;
    public int AmountPerBox 
    { 
        get { return amountPerBox; } 
        set 
        { 
            amountPerBox = value; 
        } 
    }

    public BoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int v) : base(id, name, description, price, UnitTypes.PerBox, maxAmountInStock)
    {
    }

    public string DisplayBoxedProduxtDetails()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Boxed Product\n");
        sb.Append($"Id: {Name}\n {Description}\n {Price}\n {AmountInStock} itenm(s) in stock");

        if(IsBelowStockThreshold)
        {
            sb.Append("\n*!!STOCK LOW!!");
        }
        return sb.ToString();
    
    }

    public void UseBoxedProduct(int items)
    {
        
        int smalllestMultiple= 0;
        int batchSize;

        while (true)
        {
            smalllestMultiple++;
            if (smalllestMultiple * AmountPerBox > items)
            {
                batchSize = smalllestMultiple * AmountPerBox;
                break;
            }
            
        }
        UseProduct(batchSize); //use base class method to use items
    }
}

using System;
using System.Text;
using BethanysPieShop.InventoryManagement.Contracts;
using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement
{

    public class BoxedProduct : Product, ISavable
    {
        private int amountPerBox;
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

        //public string DisplayBoxedProduxtDetails()
        // {
        //     StringBuilder sb = new StringBuilder();
        //    sb.Append("Boxed Product\n");
        //    sb.Append($"Id: {Name}\n {Description}\n {Price}\n {AmountInStock} itenm(s) in stock");

        //    if (IsBelowStockThreshold)
        //    {
        //        sb.Append("\n*!!STOCK LOW!!");
        //    }
        //    return sb.ToString();

        //}

        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Boxed Product\n");
            sb.Append($"Id: {Name}\n {Description}\n {Price}\n {AmountInStock} itenm(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n*!!STOCK LOW!!");
            }
            return sb.ToString();
        }

        public override void UseProduct(int items) //override base class method using polymorphism
        {
            int smalllestMultiple = 0;
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
            base.UseProduct(batchSize); //use base class method to use items
        }

        //public void UseBoxedProduct(int items)
        //{

        //  int smalllestMultiple= 0;
        //    int batchSize;

        //    while (true)
        //    {
        //        smalllestMultiple++;
        //       if (smalllestMultiple * AmountPerBox > items)
        //       {
        //                batchSize = smalllestMultiple * AmountPerBox;
        //          break;
        //     }

        //  } //        UseProduct(batchSize); //use base class method to use items
        //}

        public override void IncreaseStock()
        {
            AmountInStock += amountPerBox;
        }
        public override void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amountPerBox * amount;
            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amountPerBox * amount;

            }
            else
            {
                AmountInStock = maxItemsInStock; //
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }

            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }



        }

        public string ConvertToStringForSavin()
        {
            return $"{Id}; {Name}; {Description}; {maxItemsInStock};{Price.ItemPrice}; {(int) Price.Currency}; {(int) UnitType};1; {AmountPerBox}";
        }
    }

    

}


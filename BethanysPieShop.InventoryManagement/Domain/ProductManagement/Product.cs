using System.Text; // for StringBuilder
using System.Collections.Generic; // for List<T>
using System.Linq; // for LINQ methods
using System.Threading.Tasks;
using BethanysPieShop.InventoryManagement.General; // for async programming

namespace BethanysPieShop.InventoryManagement.ProductManagement
{
    public partial class Product: System.Object
    {
        private int id;
        private string name = string.Empty; // initialize to empty string
        private string? description;
        protected int maxItemsInStock = 0;
        

        public int Id 
        {
            get { return id; }
            set
            { 
                id = value; 
            }
        }
        public string Name 
        { 
            get { return name; } 
            set 
            { 
                name = value.Length > 50 ? value[..50] : value; // [..50] means take from start to 50
            } 
        }

        public string? Description 
        { 
            get { return description; } 
            set 
            { 
               if(value == null)
               {
                    description = string.Empty;
               }
               else
               {
                    description = value.Length > 250 ? value[..250] : value; 
               }
            } 
        }

        public UnitTypes UnitType {get; set;}

        public int AmountInStock{get;protected set;} // private set means it can only be changed within this class

        public bool IsBelowStockThreshold {get; protected set;}//

        public Price Price {get; set;} // Price property of type Price

         public Product(int id) : this(id, string.Empty) // constructor chaining
        {
        }

        public Product(int Id, string name) // constructor
        {
            this.Id = Id;
            this.Name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitTypes unitType, int maxAmountInStock) // constructor
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }


        public virtual void UseProduct(int items)
        {
            if(items <= AmountInStock)
            {
                AmountInStock -= items;
                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available, {items} requested.");
            }

        }

        public virtual void IncreaseStock()
        {
            AmountInStock++;
        }

        public virtual void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if(newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock; //we only store the possible maximum items
                Log($"Created {CreateSimpleProductRepresentation()}. Stock overflow {newStock - AmountInStock} item(s) ordered that could'nt be stored..");
            }

            if(AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }


        protected virtual void DecreaseStock(int items, string reason)
        {
            if(maxItemsInStock <= AmountInStock)
            {
                //decrease stock with the specified number of items
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }
            UpdateLowStock();
        }

        public virtual string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock.";
        }

        public virtual string DisplayDetailsFull()
        {

           StringBuilder sb = new();

            sb.Append($"{Id}: {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock.");

            if(IsBelowStockThreshold)
           {
             sb.Append("\n!!STOCK LOW!!");
          }
            return sb.ToString();
            //return DisplayDetailsFull("");
        }

        public virtual string DisplayDetailsFull(string extraDetails)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id}: {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock.");
            sb.Append($"\n{extraDetails}");

            if(IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }
            return sb.ToString();
            
        }
    }
}
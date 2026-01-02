using System.Text; // for StringBuilder
using System.Collections.Generic; // for List<T>
using System.Linq; // for LINQ methods
using System.Threading.Tasks; // for async programming

namespace BethanysPieShop.InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty; // initialize to empty string
        private string? description;
        private int maxItemsInStock = 0;
        

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

        public int AmountInStock{get;private set;} // private set means it can only be changed within this class

        public bool IsBelowStockThreshold {get; private set;}//

         public Product(int id) : this(id, string.Empty) // constructor chaining
        {
        }

        public Product(int id, string name) // constructor
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, int maxAmountInStock, UnitTypes unitType) // constructor
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        

        public void UseProduct(int items)
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

        public void IncreaseStock()
        {
            AmountInStock++;
        }

        public void IncreaseStock(int amount)
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

            if(AmountInStock > 10)
            {
                IsBelowStockThreshold = false;
            }
        }


        public void DecreaseStock(int items, string reason)
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

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock.";
        }

        public string DisplayDetailsFull()
        {

           // StringBuilder sb = new();

            //sb.Append($"{Id}: {name} \n{description}\n{AmountInStock} item(s) in stock.");

            //if(IsBelowStockThreshold)
           // {
             //   sb.Append("\n!!STOCK LOW!!");
          //  }
           // return sb.ToString();
            return DisplayDetailsFull("");
        }

        public string DisplayDetailsFull(string extraDetails)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id}: {name} \n{description}\n{AmountInStock} item(s) in stock.");
            sb.Append($"\n{extraDetails}");

            if(IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }
            return sb.ToString();
            
        }

        public void UpdateLowStock()
        {
            if(AmountInStock < 10) // for max value
            {
                IsBelowStockThreshold = true;
                
            }
            
        }

        public void Log(string message)
        {
            //this coild be written to a file or database instead
            Console.WriteLine(message);
        }

        public string CreateSimpleProductRepresentation()
        {
            return $"Product: {id} ({name})";
        }
    }
}
using System.Text; // for StringBuilder
using System.Collections.Generic; // for List<T>
using System.Linq; // for LINQ methods
using System.Threading.Tasks;
using BethanysPieShop.InventoryManagement.General; // for async programming

namespace BethanysPieShop.InventoryManagement.ProductManagement
{
    public partial class Product
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
    }
}
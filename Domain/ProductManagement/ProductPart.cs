using System.Text; // for StringBuilder
using System.Collections.Generic; // for List<T>
using System.Linq; // for LINQ methods
using System.Threading.Tasks;
using BethanysPieShop.InventoryManagement.General; // for async programming

 
namespace BethanysPieShop.InventoryManagement.ProductManagement
{
    public partial class Product
    {
        public static int StockThreshold = 5; // static field for stock threshold

        public static void ChangeStockThreshold(int newStockThreshold)
        {
            if(newStockThreshold >= 0) // only allow this to go throught the value is >0
            {
                StockThreshold = newStockThreshold;
            }
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

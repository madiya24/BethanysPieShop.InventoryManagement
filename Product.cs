namespace BethanysPieShop.InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;
        private int maxItemsInStock = 0;
        private UnitTypes unitType ;
        private int amountInStock = 0;
        private bool isBelowStockThreshold = false;

        public void UseProduct(int items)
        {
            if(items <= amountInStock)
            {
                amountInStock -= items;
                UpdateLowStock();

                Log($"Amount in stock updated. Now {amountInStock} items in stock.");
            }else
            {
                Log($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {amountInStock} available, {items} requested.");
            }

        }

        public void IncreaseStock()
        {
            amountInStock++;
        }

        public void UpdateLowStock()
        {
            if(amountInStock < 10) // for max value
            {
                isBelowStockThreshold = true;
                
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
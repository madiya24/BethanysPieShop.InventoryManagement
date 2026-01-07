using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;
using System.IO;    
//using BethanysPieShop.InventoryManagement.OrderManagement;

namespace BethanysPieShop.InventoryManagement
{
    internal class ProductRepository
    {
        private string directory = @"/Users/chrines/Desktop/Personal_Dev/BethanysPieShop.InventoryManagement/BethanysPieShop/"; // create a directory path
        private string productsFileName = "products.txt"; // file name for products

        private void CheckForExistingProductFile() // checks if the product file exists, if not creates it
        {
            string path = $"{directory}{productsFileName}";

            bool exixtingFileFound = File.Exists(path);
            if (!exixtingFileFound)
            {
                // create directory if it doesn't exist
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);

                    //crate the empty file
                    using FileStream fs = File.Create(path);
                }
            }
        }

        public List<Product> LoadProductsFromFile()
        {
           List<Product> products = new List<Product>();

           string path = $"{directory}{productsFileName}";
            try
            {
                CheckForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(path);
                for(int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    bool success = int.TryParse(productSplits[0], out int productId);
                    if(!success)
                    {
                       productId = 0;
                    }
                    string name = productSplits[1];
                    string description = productSplits[2];

                    success = int.TryParse(productSplits[3], out int maxItemsInStock);
                    if(!success)
                    {
                       maxItemsInStock = 100;
                    }
                    success = int.TryParse(productSplits[4], out int ItemPrice);
                    if(!success)
                    {
                       ItemPrice = 0; // default price
                    }
                    success = Enum.TryParse(productSplits[5], out Currency currency);
                    if(!success)
                    {
                       currency = Currency.Dollar;
                    }

                    success = Enum.TryParse(productSplits[6], out UnitTypes unitType);
                    if(!success)
                    {
                       unitType = UnitTypes.PerItem; // default unit type
                    }

                    Product product = new Product(productId, name, description, new Price(){ItemPrice = ItemPrice, Currency = currency}, unitType, maxItemsInStock);
                    products.Add(product); // add product to local list
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsing the file, please check the data.");
                Console.WriteLine(iex.Message);
            }
            catch(FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product file not found.");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong loading the products.");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
              return products;
        }

    }
}   
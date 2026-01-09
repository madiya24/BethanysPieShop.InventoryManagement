using BethanysPieShop.InventoryManagement.Domain.ProductManagement;
using BethanysPieShop.InventoryManagement.General; // for async programming
//using BethanysPieShop.InventoryManagement.Domain.OrderManagement;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.ProductManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();


        internal static void InitializeStock() // Mock implementation
        {
            //BoxedProduct bp = new BoxedProduct(6, "Eggs", "Lorem ipsum", new Price() { ItemPrice = 10, Currency = Currency.Euro }, 100, 6);
            //bp.IncreaseStock(100);// increase stock by 100
            //bp.UseProduct(10); // use 20 items from stock

           // bp.ToString();
           RegularProduct rp = new RegularProduct(7,"Pie candles", "Lorem ipsum", new Price(){ItemPrice = 10, Currency = Currency.Euro} ,UnitTypes.PerItem,100);
           double value = rp.ConverProductPrice(Currency.Dollar);

            ProductRepository productRepository = new();
            inventory = productRepository.LoadProductsFromFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventory.Count} products!.");

            Console.WriteLine("Press to continue!");
            Console.ResetColor();
            Console.ReadLine();

        }

        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("****************************");
            Console.WriteLine("** Select an action **");
            Console.WriteLine("****************************");

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");

            Console.Write("Enter your choice: ");// prompt user for input

            string? userSelection = Console.ReadLine(); // read user input
            switch(userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ShowSettingsMenu();
                    break;
                case "4":
                    // Save all data
                    break;
                case "0":
                    // Close application
                    break;
                default:
                    Console.WriteLine("Invalid selection. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }

        private static void ShowInventoryManagementMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("****************************");
                Console.WriteLine("** Inventory Management **");
                Console.WriteLine("****************************");

                ShowAllProductsOverview();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: View details products");
                Console.WriteLine("2: Add new products");
                Console.WriteLine("3: Close product");
                Console.WriteLine("4: View product with low stock");
                Console.WriteLine("0: Return to main menu");

                Console.Write("Enter your choice: ");// prompt user for input
                userSelection = Console.ReadLine(); // read user input

                switch(userSelection)
                {
                    case "1":
                       ShowDetailsAndUserProduct();
                        break;
                    case "2":
                        ShowCreateNewProduct();
                        break;
                    case "3":
                        // ShowCloneExistingProduct();
                        break;
                    case "4":
                        ShowProductsLowOnStock();
                        break;
                    case "0":
                        // Return to main menu
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }

            }
            while(userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowAllUnitTypes()
        {
            int i = 1;
            foreach(string name in Enum.GetNames(typeof(UnitTypes)))
            {
                Console.WriteLine($"{i}: {name}");
                i++;
            }
            
        }

        private static void ShowAllCurrencies()
        {
            int i = 1;
            foreach(string name in Enum.GetNames(typeof(Currency)))
            {
                Console.WriteLine($"{i}: {name}");
                i++;
            }
            
        }   

        private static void ShowCreateNewProduct()
        {
            UnitTypes unitType = UnitTypes.PerItem;

            Console.WriteLine("What type of product do you want to create?");
            Console.WriteLine("1: Regular product\n2. Bilk product\n3. Fresh product\n4. Boxed product");
            Console.Write("Enter your choice: ");// prompt user for input

            var productType = Console.ReadLine(); // read user input
            if (productType != "1" && productType != "2" && productType != "3" && productType != "4")
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Product? newProduct = null;

            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine()?? string.Empty; // read name

            Console.WriteLine("Enter product price: ");
            double Price = double.Parse(Console.ReadLine()?? "0.0"); // read price

            ShowAllCurrencies();
            Console.Write("Select currency (enter number): ");
            Currency curruncy = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine()?? "1"); // read currency

            Console.WriteLine("Enter product description: ");
            string description = Console.ReadLine()?? string.Empty; // read description

            if (productType == "1")
            {
               ShowAllUnitTypes();
               Console.Write("Select unit type (enter number): ");
               unitType = (UnitTypes)Enum.Parse(typeof(UnitTypes), Console.ReadLine()?? "1"); // read unit type
            }

            Console.WriteLine("Enter max amount in stock for product: ");
            int maxInStock = int.Parse(Console.ReadLine()?? "0"); // read max amount in stock
            int newId = inventory.Max(p => p.Id) + 1; // find max id and increment by 1

            switch(productType)
            {
                case "1":
                    newProduct = new RegularProduct(newId, name, description, new Price() { ItemPrice = Price, Currency = curruncy }, unitType, maxInStock);
                    break;
                case "2":
                    newProduct = new BulkProduct(newId, name, description, new Price() { ItemPrice = Price, Currency = curruncy }, maxInStock);
                    break;
                case "3":
                    newProduct = new FreshProduct(newId, name, description, new Price() { ItemPrice = Price, Currency = curruncy }, unitType, maxInStock);
                    break;
                case "4":
                    Console.WriteLine("Enter amount items per box: ");
                    int amountPerBox = int.Parse(Console.ReadLine()?? "0"); // read amount per box
                    newProduct = new BoxedProduct(newId, name, description, new Price() { ItemPrice = Price, Currency = curruncy }, maxInStock, amountPerBox);
                    break;
            }
            if(newProduct != null)
            inventory.Add(newProduct);
                
            
            
        }

        private static void ShowAllProductsOverview()
        {
            foreach(var product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }

        private static void ShowDetailsAndUserProduct()
        {
            string? userSelection = string.Empty;
            Console.Write("Enter the product ID : ");
            string? selectedProductId = Console.ReadLine();

            if(selectedProductId != null)
            {
                Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                if(selectedProduct != null)
                {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());

                    Console.WriteLine("\n What do you want to do?");
                    Console.WriteLine("1: Use product");
                    Console.WriteLine("0: Return to inventory overview");

                    Console.Write("Enter your choice: ");// prompt user for input
                    userSelection = Console.ReadLine(); // read user input

                    if(userSelection == "1")
                    {
                        Console.WriteLine("How many product do you want to use?");
                        int amount = int.Parse(Console.ReadLine() ?? "0"); // read amount

                        selectedProduct.UseProduct(amount);
                        Console.ReadLine();
                    }   
                }
                
            }
            else
            {
                Console.WriteLine("Product not found. Try agin.");
                
            }
        }

        private static void ShowProductsLowOnStock()
        {
            List<Product> lowOnStockProducts = inventory.Where(p => p.IsBelowStockThreshold).ToList(); // using LINQ to filter products below stock threshold

            if(lowOnStockProducts.Count > 0)
            {
                Console.WriteLine("The following products are low on stock, order more soon!");

                foreach(var product in lowOnStockProducts) //
                {
                    Console.WriteLine(product.DisplayDetailsShort());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There are no products low on stock.");
            }
            Console.ReadLine();
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("****************************");
                Console.WriteLine("** Select an action **");
                Console.WriteLine("****************************");

                Console.WriteLine("1: Open orders overview");
                Console.WriteLine("2: Add new order");
                Console.WriteLine("0: Return to main menu");

                Console.Write("Enter your choice: ");// prompt user for input
                userSelection = Console.ReadLine(); // read user input

                switch(userSelection)
                {
                    case "1":
                        ShowOpenOrderOverview();
                        break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }

            }
            while(userSelection != "0");
            ShowMainMenu();
        }

        public static void ShowOpenOrderOverview()
        {   // Check for fulfilled orders first
            ShowFulfilledOrders();

            if(orders.Count > 0)
            {
                Console.WriteLine("Open oders");

                foreach(var order in orders)
                {
                    Console.WriteLine(order.ShowOrderDertails());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There are no open orders");
            }
            Console.ReadLine();
        }

        private static void ShowFulfilledOrders()
        {
            Console.WriteLine("Checking fulfilled orders:");
            foreach(var order in orders)
            {
                if(order.Fulfilled && order.OrderFulfilmentDate <= DateTime.Now) // check if order is fulfilled
                {
                    foreach(var orderItem in order.OrderItems)
                    {
                        Product? selectedProduct = inventory.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();
                        if(selectedProduct != null)
                        {
                            selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                        }
                    }
                    order.Fulfilled = true;
                }
            }
            orders.RemoveAll(o => o.Fulfilled); // remove fulfilled orders from the list
            Console.WriteLine("Fulfilled orders checked.");
        }

        private static void ShowAddNewOrder()
        {
            Order newOrder = new Order();
            string? selectedProductId = string.Empty;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Create new order");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();
                Console.Write("Which product do you want to add to the order? (Enter  0 to stop adding products to stock ): ");
                Console.Write("Enter product ID: ");
                selectedProductId = Console.ReadLine();

                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.Write("How many items do you want to order? ");
                        int amount = int.Parse(Console.ReadLine() ?? "0");
                        
                        OrderItem orderItem = new OrderItem
                        {
                            ProductId = selectedProduct.Id,
                            ProductName = selectedProduct.Name,
                            AmountOrdered = amount
                        };
                    }
                }
            } while (selectedProductId != "0");
        }

        private static void ShowSettingsMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("****************************");
                Console.WriteLine("** Settings **");
                Console.WriteLine("****************************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Change stock threshold");
                Console.WriteLine("0: Return to main menu");

                Console.Write("Enter your choice: ");// prompt user for input
                userSelection = Console.ReadLine(); // read user input

                switch(userSelection)
                {
                    case "1":
                        ShowChangeStockThreshold();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to try again.");
                        break;
                }

            }
            while(userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowChangeStockThreshold()
        {
            Console.Write($"Enter new stock threshold (current value: {Product.StockThreshold}). This applies to all products: ");
            Console.Write("new ValueTask: ");
            int newThreshold = int.Parse(Console.ReadLine() ?? "0");
            Product.StockThreshold = newThreshold;
            Console.WriteLine($"Stock threshold updated to {Product.StockThreshold}.");

            foreach(var product in inventory)
            {
                product.UpdateLowStock(); // update low stock status for all products
            }
            Console.ReadLine();
        }
    }
}
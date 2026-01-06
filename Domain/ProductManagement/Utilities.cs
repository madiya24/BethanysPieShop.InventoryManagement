using BethanysPieShop.InventoryManagement.General; // for async programming
//using BethanysPieShop.InventoryManagement.OrderManagement;
using BethanysPieShop.InventoryManagement.ProductManagement;

namespace BethanysPieShop.InventoryManagement.ProductManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();

        internal static void InitializeStock() // Mock implementation
        {
            Product p1 = new Product(1) {Name = "Sugar", Description = "Lemon ipsum", Price = new Price() {ItemPrice = 10, Currency = Currency.Euro}, UnitType = UnitTypes.PerKg}; // create Product object
            p1.IncreaseStock(10); // increase stock by 10

            var p2 = new Product(2) {Name = "Cake", Description = "Lemon ipsum", Price = new Price() {ItemPrice = 8, Currency = Currency.Euro}, UnitType = UnitTypes.PerItem}; // create another Product object
            p2.IncreaseStock(5); // increase stock by 5

            Product p3 = new Product(3) {Name = "Strawberry", Description = "Lemon ipsum", Price = new Price() {ItemPrice = 3, Currency = Currency.Euro}, UnitType = UnitTypes.PerBox}; // create another Product object
            p3.IncreaseStock(20); // increase stock by 20

            inventory.Add(p1);
            inventory.Add(p2);
            inventory.Add(p3);
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
                        // Add new products
                        break;
                    case "3":
                        // Close product
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
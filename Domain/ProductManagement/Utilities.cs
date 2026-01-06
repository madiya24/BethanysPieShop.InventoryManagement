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
            ShowFulfillmentOrders();

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

        public static void ShowOpenOrderOverview()
        {
            ShowFulfillmentOrders();

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

        private static void ShowFulfillmentOrders()
        {
            Console.WriteLine("Products below stock threshold:");

            var lowStockProducts = inventory.Where(p => p.IsBelowStockThreshold);

            foreach(var product in lowStockProducts)
            {
                Console.WriteLine(product.CreateSimpleProductRepresentation());
            }
            Console.WriteLine();
        }
    }
}
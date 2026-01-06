using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

Product.ChangeStockThreshold(10); // change stock threshold to 10

//Price samplePrice = new Price(10, Currency.Euro); // create Price object
Price samplePrice = new Price() {ItemPrice = 10, Currency = Currency.Euro}; // create object using object initializer syntax

Product p1 = new Product(1) {Name = "Sugar", Description = "Lemon ipsum", Price = samplePrice, UnitType = UnitTypes.PerKg}; // create Product object
p1.IncreaseStock(10); // increase stock by 10
p1.Description = "Sample description";

var p2 = new Product(2) {Name = "Cake", Description = "Lemon ipsum", Price = new Price() {ItemPrice = 8, Currency = Currency.Euro}, UnitType = UnitTypes.PerItem}; // create another Product object
Product p3 = new Product(3) {Name = "Strawberry", Description = "Lemon ipsum", Price = new Price() {ItemPrice = 3, Currency = Currency.Euro}, UnitType = UnitTypes.PerBox}; // create another Product object


PrintWelcome();

Utilities.InitializeStock();
Utilities.ShowMainMenu();
Console.WriteLine("Application shutting down...");
Console.ReadLine();

static void PrintWelcome()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(@"");
}
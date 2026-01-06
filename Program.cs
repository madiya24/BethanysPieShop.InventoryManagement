using BethanysPieShop.InventoryManagement.General;
using BethanysPieShop.InventoryManagement.ProductManagement;

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
namespace BethanysPieShop.InventoryManagement.General
{
    public class Price
    {
        public Double ItemPrice { get; set; }
        public Currency Currency { get; set; } // Currency property of type Currency enum

        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }

        public Price (){}
        
        public Price(Double itemPrice, Currency currency)
        {
            ItemPrice = itemPrice;
            Currency = currency;
        }
    }
}
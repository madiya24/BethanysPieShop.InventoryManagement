using System.Text; // for StringBuilder
namespace BethanysPieShop.InventoryManagement.General
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<OrderItem> OrderItems { get;  } 

        public bool Fulfilled {get; set;} = false;

        public Order()
        {
            Id = new Random().Next(9999999); // random id for demo purposes

            int numberofSeconds = new Random().Next(100);
            OrderFulfilmentDate = DateTime.Now.AddSeconds(numberofSeconds);
            
            OrderItems = new List<OrderItem>();
        }

        public string ShowOrderDertails()
        {
            StringBuilder orderDetails = new StringBuilder();

            orderDetails.AppendLine($"Order Id: {Id}");
            orderDetails.AppendLine($"Fulfillment Date: {OrderFulfilmentDate.ToShortTimeString()}");
            
            if(OrderItems != null)
            {
                foreach(var item in OrderItems)
                {
                    orderDetails.AppendLine($" {item.ProductId} .{item.ProductName}, {item.AmountOrdered} ");
                }
            }
            return orderDetails.ToString();
            
        }
        
    }
}
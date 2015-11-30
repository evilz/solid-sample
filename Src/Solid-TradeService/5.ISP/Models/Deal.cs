using System;
namespace TradeApp.Models
{
    public class Deal
    {
        private Deal(string id, double price,  int quantity, string symbol, DateTime time)
        {
            Id = id;
            Price = price;
            Time = time;
            Quantity = quantity;
            Symbol = symbol;
        }

        public static Deal Empty
        {
            get { return new Deal("Empty", 0, 0, "Unset", DateTime.MinValue); }
        }

        public string Id { get; set; }
        public double Price { get; set; }
        public DateTime Time { get; set; }
        public int Quantity { get; set; }

        public string Symbol { get; set; }

        public OrderDirection Direction
        {
            get {
                return Quantity >= 0 ? OrderDirection.Buy : OrderDirection.Sell;
            }
        }

        public Deal With(string id = null, double? price = null, int? quantity = null, string symbol = null, DateTime? time = null)
        {
            return new Deal(
                id ?? Id,
                price ?? Price,
                quantity ?? Quantity,
                symbol ?? Symbol,
                time ?? Time
                );

        }
        
        public override string ToString()
        {
            return string.Format("OrderId:  {0} {1} order for {2} unit{3} of {4}", Id, Direction, Quantity,
                Quantity == 1 ? "" : "s", Symbol);
        }
    }
}
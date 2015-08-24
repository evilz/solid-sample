using System;
namespace TradeApp.Models
{
    public class Deal : IIdentifiable
    {
        private Deal(string id, double price,  int quantity, string symbol, DateTime time)
        {
            Id = id;
            Price = price;
            Time = time;
            Quantity = quantity;
            Symbol = symbol;
        }

        public static Deal Empty => new Deal("Empty", 0, 0, "Unset", DateTime.MinValue);

        public string Id { get;  }
        public double Price { get;  }
        public DateTime Time { get; }
        public int Quantity { get;  }

        public string Symbol { get; }
       
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
            return $"OrderId:  {Id} {Direction} order for {Quantity} unit{(Quantity == 1 ? "" : "s")} of {Symbol}";
        }
    }
}
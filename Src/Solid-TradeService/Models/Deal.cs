using System;
namespace TradeApp.Models
{
    public class Deal
    {
        public string Id { get; set; }
        public double Price { get;  set; }
        public DateTime Time { get;  set; }
        public int Quantity { get;  set; }
        public string Symbol { get; set; }
        public OrderDirection Direction { get; set; }

        public override string ToString()
        {
            return string.Format("OrderId:  {0} {1} order for {2} unit{3} of {4}", Id,Direction, Quantity, Quantity == 1 ? "" : "s", Symbol);
        }
    }
}
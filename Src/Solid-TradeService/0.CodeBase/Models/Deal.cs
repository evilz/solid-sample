using System;
namespace TradeApp.Models
{
    public class Deal
    {
        public string Id { get; set; }
        public double Price { get; internal set; }
        public DateTime Time { get; internal set; }
        public int Quantity { get; internal set; }

        public string Symbol { get; set; }
       
        public OrderDirection Direction
        {
            get {
                return Quantity >= 0 ? OrderDirection.Buy : OrderDirection.Sell;
            }
        }
        public override string ToString()
        {
            return string.Format("OrderId:  {0} {1} order for {2} unit{3} of {4}", Id,Direction, Quantity, Quantity == 1 ? "" : "s", Symbol);
        }
    }
}
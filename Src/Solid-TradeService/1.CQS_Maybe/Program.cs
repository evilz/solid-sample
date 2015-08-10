using System;
using System.Linq;
using TradeApp.App_Packages.LibLog._4._2;
using TradeApp.Models;

namespace TradeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = Environment.UserName;

            Logging.LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider()); 

            DealService service = new DealService(username);
            
            var id = Guid.NewGuid().ToString();
            var deal = new Deal
            {
                Id = id,
                Price = 500.00,
                Quantity = 10,
                Symbol = "MSFT",
                Time = DateTime.Now
            };

            service.Save(id, deal);

            Console.WriteLine(service.GetFileName(id));

            Console.WriteLine();
            Console.WriteLine();


            var defaultDeal = new Deal
            {
                Id = "UnSet",
                Price = 0,
                Quantity = 0,
                Symbol = "UnSet",
                Time = DateTime.MinValue
            };
            var vincentDeal =  service.Load(id).DefaultIfEmpty(defaultDeal).Single();
           Console.WriteLine(vincentDeal);

           Console.ReadLine();
        }
    }
}

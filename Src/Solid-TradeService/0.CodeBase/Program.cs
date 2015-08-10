using System;
using TradeApp.App_Packages.LibLog._4._2;
using TradeApp.Models;

namespace TradeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging.LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider()); 

            DealService service = new DealService();

            service.Username = "Vincent";
            var id = Guid.NewGuid().ToString();
            var deal = new Deal
            {
                Id = id,
                Price = 500.00,
                Quantity = 10,
                Symbol = "MSFT",
                Time = DateTime.Now
            };
            var result = service.Save(id, deal);

            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine();

           var vincentDeal =  service.Load(id);
           Console.WriteLine(vincentDeal);

            Console.ReadLine();
        }
    }
}

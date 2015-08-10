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
            var deal = Deal.Empty.With(
                id,
                500.00,
                10,
                "MSFT",
                DateTime.Now
            );

            service.Save(id, deal);
            

            Console.WriteLine();
            Console.WriteLine();

            var vincentDeal =  service.Load(id).DefaultIfEmpty(Deal.Empty).Single();
           Console.WriteLine(vincentDeal);

           Console.ReadLine();
        }
    }
}

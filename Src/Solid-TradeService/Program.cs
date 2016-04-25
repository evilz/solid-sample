using System;
using TradeApp.App_Packages.LibLog._4._2;
using TradeApp.Models;

namespace TradeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // init loggin
            Logging.LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());

            // create service
            var service = new DealService { Username = "Vincent" };

            // create a new id
            var id = Guid.NewGuid().ToString();

            // create a new deal
            var deal = new Deal
            {
                Id = id,
                Price = 500.00,
                Quantity = 10,
                Symbol = "MSFT",
                Time = DateTime.Now
            };

            // save the deal
            var result = service.Save(id, deal);

            // display result
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            // try reload the deal
            var vincentDeal = service.Load(id);

            // show the loaded deal !
            Console.WriteLine(vincentDeal);


            // wait for input
            Console.ReadLine();
        }
    }
}

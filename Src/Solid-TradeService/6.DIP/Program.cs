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

            var serializer = new DealSerializer();
            var dealStorage = new FileDealStorage(username,serializer);
            var dealCache = new DealCaching(dealStorage,dealStorage);
            var dealLogger = new DealServiceLogger(dealCache,dealCache);

            var dealService = new DealService(dealLogger,dealLogger);

            var id = Guid.NewGuid().ToString();
            var deal = Deal.Empty.With(
                id,
                500.00,
                10,
                "MSFT",
                DateTime.Now
            );

            dealService.Save(new Maybe<Deal>(deal));
            

            Console.WriteLine();
            Console.WriteLine();

            var vincentDeal = dealService.Load(id).DefaultIfEmpty(Deal.Empty).Single();
           Console.WriteLine(vincentDeal);


            var unknownDeal = dealService.Load("Fizz");

            dealService.Save(unknownDeal);

           Console.ReadLine();
        }
    }
}

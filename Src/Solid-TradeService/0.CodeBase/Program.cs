using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using TradeApp.Models;

namespace TradeApp
{
    class Program
    {

        private static readonly ILogger _logger = new ConsoleLogger(nameof(Program), (category, logLevel) => logLevel >= LogLevel.Trace, true);

        static void Main(string[] args)
        {


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

            _logger.LogInformation(result);

           var vincentDeal =  service.Load(id);
           _logger.LogInformation(vincentDeal.ToString());

            Console.ReadLine();
        }
    }
}

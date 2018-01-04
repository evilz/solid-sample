using System;
using Functional.Maybe;
using SimpleInjector;
using TradeApp.App_Packages.LibLog._4._2;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var dealService = ConfigureRoot<DealService>();

            var id = Guid.NewGuid().ToString();
            var deal = Deal.Empty.With(
                id,
                500.00,
                10,
                "MSFT",
                DateTime.Now
            );

            dealService.Save(deal.ToMaybe());


            Console.WriteLine();
            Console.WriteLine();

            var vincentDeal = dealService.Load(id).Or(Deal.Empty);
            Console.WriteLine(vincentDeal);


            var unknownDeal = dealService.Load("Fizz");

            dealService.Save(unknownDeal);

            Console.ReadLine();
        }

        private static TRoot ConfigureRoot<TRoot>() where TRoot : class
        {
            LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());
            
            var container = new Container();

            container.Register<IUsersManager, UsersManager>(Lifestyle.Singleton);
            container.Register(typeof(ISerializer<Deal>), typeof(JsonSerializer<Deal>),Lifestyle.Singleton);
            container.Register(typeof(IFileLocator<Deal>),typeof(FileStorage<Deal>),Lifestyle.Singleton);
            container.Register(typeof(IReadWrite<Deal>),typeof(FileStorage<Deal>),Lifestyle.Singleton);
            
            container.RegisterDecorator(typeof(IReadWrite<Deal>), typeof(Caching<Deal>));
            container.RegisterDecorator(typeof(IReadWrite<Deal>), typeof(Logger<Deal>));
            
            container.Register<TRoot>();
            return container.GetInstance<TRoot>();
        }
    }

}

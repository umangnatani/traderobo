using System;
using TradeRobo.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TradeRoboJob
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApplication>().Run();


        }


        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<MyDatabaseContext>(options =>
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=coreDB;User ID=sa;Password=traderno1#;Encrypt=true;Connection Timeout=30;TrustServerCertificate=True"));

            // IMPORTANT! Register our application entry point
            services.AddTransient<ConsoleApplication>();
            return services;
        }

    }


    public class ConsoleApplication
    {
        private readonly MyDatabaseContext _context;

        public ConsoleApplication(MyDatabaseContext context)
        {
            _context = context;
        }

        public void Run()
        {

            TradeService service = new TradeService(_context, 1);

            var rt = service.RunSchedule();

            Console.WriteLine(rt.Message);

        }
    }

}

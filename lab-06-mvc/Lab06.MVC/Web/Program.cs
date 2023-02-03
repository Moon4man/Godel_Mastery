using Lab06.MVC.Infrastructure.Data;
using Serilog;
using System.Text;

namespace Lab06.MVC.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var host = CreateWebHostBuilder(args).Build();

            Log.Information("Seeding Database...");

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ShopDBContext>();
                    DbObject.Initial(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding in DB.");
                }
            }

            Log.Information("LAUNCHING");
            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

using Serilog;


namespace TestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("file.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            try
            {
                Log.Logger = loggerConfiguration;

                IHost host = CreateHostBuilder(args).Build();
                host.Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })             
                .UseSerilog();      
    } 
}

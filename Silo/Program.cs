using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;


namespace Introduction
{
    public class Program
    {
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=saorleans;AccountKey=AC6YBdzSWcKtLj/3hU3vaFKrvhqMRoW0cZEBc1RcrNgVF3F8cRBErlhykUE4vlqn6MpE1FG2XL7z4taEm0Aw4A==;EndpointSuffix=core.windows.net";
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TutorialGrain).Assembly).WithReferences())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(UnitTestingGrain).Assembly).WithReferences())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(CallingGrain).Assembly).WithReferences())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TimerGrain).Assembly).WithReferences())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ReminderGrain).Assembly).WithReferences())
                .UseInMemoryReminderService()
                //.AddMemoryGrainStorage(Constants.OrleansMemoryProvider)
                //.UseAzureTableReminderService(options => options.ConnectionString = connectionString)
                .ConfigureLogging(logging => logging.AddConsole());


            var host = builder.Build();

            await host.StartAsync();
            return host;
        }

        //private static async Task<ISiloHost> StartSilo()
        //{
        //    // define the cluster configuration
        //    var builder = new SiloHostBuilder()
        //     .UseLocalhostClustering()
        //     .Configure<ClusterOptions>(options =>
        //     {
        //         options.ClusterId = "dev";
        //         options.ServiceId = "HelloWorldApp";
        //     })
        //     .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        //     .AddMemoryGrainStorage(Constants.OrleansMemoryProvider)
        //     .ConfigureApplicationParts(parts =>
        //     {
        //         parts.AddApplicationPart(typeof(TutorialGrain).Assembly).WithReferences();
        //         parts.AddApplicationPart(typeof(UnitTestingGrain).Assembly).WithReferences();
        //         parts.AddApplicationPart(typeof(CallingGrain).Assembly).WithReferences();
        //         parts.AddApplicationPart(typeof(TimerGrain).Assembly).WithReferences();
        //         parts.AddApplicationPart(typeof(ReminderGrain).Assembly).WithReferences();
        //     })
        //     .ConfigureServices(DependencyInjectionHelper.IocContainerRegistration)
        //     .UseDashboard(options => { })
        //     .UseInMemoryReminderService()
        //     .ConfigureLogging(logging => logging.AddConsole());
        //    var host = builder.Build();
        //    await host.StartAsync();
        //    return host;
        //}
    }
}


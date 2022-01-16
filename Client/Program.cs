using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;
using GrainInterfaces;

namespace Introduction
{
    public class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    //Calling the method for use to interface with grains
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            bool repeat = true;
            bool timerStarted = false;
            ITimerGrain timer = null;
            

            Console.WriteLine("Start Reminder Grain? Y/N");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                IReminderGrain reminderGrain = client.GetGrain<IReminderGrain>("reminderAction");
                await reminderGrain.SendMessage();
            }

            Console.WriteLine("Stop Reminder Grain? Y/N");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                IReminderGrain reminderGrain = client.GetGrain<IReminderGrain>("reminderAction");
                await reminderGrain.StopMessage();
            }

            Console.WriteLine("Start timer Grain? Y/N");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                timer = client.GetGrain<ITimerGrain>(Guid.Empty);
                timer.StartTimer();
                timerStarted = true;
            }

            do
            {
                // Grain Identity
                Console.WriteLine("What is the grain id?");
                string grainId = Console.ReadLine();

                // Call Specific Grain
                var example = client.GetGrain<ITutorialGrain>(grainId);

                //Send Message with time stamp
                var response = await example.RespondWIthCurrentTime($" This is {grainId} at {DateTime.Now}");

                //View response
                Console.WriteLine($"\n\n{response}\n\n");

                //Continue the example
                Console.WriteLine("Continue? Y/N");
                string continueResponse = Console.ReadLine();

                if (continueResponse.ToUpper() == "N")
                {
                    repeat = false;
                }


                if (timerStarted)
                {
                    //Continue Timer
                    Console.WriteLine("Continue Timer? Y/N");

                    if (Console.ReadLine()?.ToUpper() == "N")
                    {
                        timer.StopTimer();
                        timerStarted = false;
                    }
                }


            } while (repeat);

            return;
        }
    }
}
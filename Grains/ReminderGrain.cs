using System;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;


namespace Introduction
{
    //[StorageProvider(ProviderName = "OrleansMemoryProvider")]
    public class ReminderGrain : Orleans.Grain, IReminderGrain
    {
        const string ReminderName = "reminderMessage";

        //Action to take when reminder is triggered
        public Task ReceiveReminder(string reminderName, TickStatus status)
        {
            //Determine 
            if (reminderName == ReminderName)
            {
                Console.WriteLine($"Reminder message created at: {DateTime.Now}");
            }

            return Task.CompletedTask;
        }

        //Register the reminder to intially start in 30 min and 1 hour thereafter
        public Task SendMessage()
        {
            return RegisterOrUpdateReminder(ReminderName, TimeSpan.FromMinutes(30), TimeSpan.FromHours(1));
        }

        //Unregister the reminder
        public async Task StopMessage()
        {
            foreach (var reminder in await GetReminders())
            {
                if (reminder.ReminderName == ReminderName)
                {
                    await UnregisterReminder(reminder);
                }
            }
        }
    }
}

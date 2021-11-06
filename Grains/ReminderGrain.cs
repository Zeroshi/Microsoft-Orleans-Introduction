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
        IGrainReminder _reminder = null;

        public async Task Start()
        {
            if (_reminder != null)
            {
                return;
            }

            _reminder = await RegisterOrUpdateReminder(
                this.GetPrimaryKeyString(),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromMinutes(1) // apparently the minimum
            );
        }

        public async Task Stop()
        {
            if (_reminder == null)
            {
                return;
            }

            await UnregisterReminder(_reminder);
            _reminder = null;
        }

        public override async Task OnActivateAsync()
        {
            await RegisterOrUpdateReminder("MyReminder", TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(2));
            await UnregisterReminder(_reminder);
 
            await base.OnActivateAsync();
        }

        public Task ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine($"This is a reminder at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}


//[StorageProvider(ProviderName = Constants.OrleansMemoryProvider)]
//public class EverythingIsOkGrain : Grain, IEverythingIsOkGrain
//{
//    IGrainReminder _reminder = null;
//    public async Task ReceiveReminder(string reminderName, TickStatus status)
//    {
//        // Grain-ception!
//        var emailSenderGrain = GrainFactory
//         .GetGrain<IEmailSenderGrain>(Guid.Empty);
//        await emailSenderGrain.SendEmail(
//         "homer@anykey.com",
//         new[]
//         {
//    "marge@anykey.com",
//    "bart@anykey.com",
//    "lisa@anykey.com",
//    "maggie@anykey.com"
//         },
//         "Everything's ok!",
//         "This alarm will sound every 1 minute, as long as everything is ok!"
//        );
//    }
//    public async Task Start()
//    {
//        if (_reminder != null)
//        {
//            return;
//        }
//        _reminder = await RegisterOrUpdateReminder(
//         this.GetPrimaryKeyString(),
//         TimeSpan.FromSeconds(3),
//         TimeSpan.FromMinutes(1) // apparently the minimum
//        );
//    }
//    public async Task Stop()
//    {
//        if (_reminder == null)
//        {
//            return;
//        }
//        await UnregisterReminder(_reminder);
//        _reminder = null;
//    }
//}
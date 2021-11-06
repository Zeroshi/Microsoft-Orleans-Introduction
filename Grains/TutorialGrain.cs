using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Orleans;

namespace Introduction
{
    public class TutorialGrain : Orleans.Grain, ITutorialGrain
    {
        private readonly ILogger logger;

        //logger instance is set through the constructor
        public TutorialGrain(ILogger<TutorialGrain> logger)
        {
            this.logger = logger;
        }

        public async Task<string> RespondWIthCurrentTime(string message)
        {
            logger.LogInformation($"\n message received: {message}");
            return await Task.FromResult($"\n '{message}' was received at {DateTime.Now}");
        }
    }
}

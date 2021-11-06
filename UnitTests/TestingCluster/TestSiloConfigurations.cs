using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;
using Orleans.TestingHost;

namespace UnitTests
{
    public class TestSiloConfigurations : ISiloBuilderConfigurator
    {
        public void Configure(ISiloHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                //Services that need to talk to grains can be placed here
                //services.AddSingleton<IService, Service>();
            });
        }
    }
}

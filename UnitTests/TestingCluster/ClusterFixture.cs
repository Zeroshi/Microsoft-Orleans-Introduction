using Orleans;
using Orleans.TestingHost;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class ClusterFixture : IDisposable
{
    public ClusterFixture()
    {
        var builder = new TestClusterBuilder();
        //Setting up TestSiloConfigurations allows us to configure silos in the cluster
        //builder.AddSiloBuilderConfigurator<TestSiloConfigurations>();
        this.Cluster = builder.Build();
        this.Cluster.Deploy();

    }

    public void Dispose()
    {
        this.Cluster.StopAllSilos();
    }

    public TestCluster Cluster { get; private set; }
    public int TestClusterId { get; private set; }
}
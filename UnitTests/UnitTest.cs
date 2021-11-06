using System;
using System.Threading.Tasks;
using Introduction;
using Orleans.TestingHost;
using Xunit;
using Moq;
using Orleans;

namespace UnitTests
{
    [Collection(ClusterCollection.Name)]
    public class UnitGrainTest
    {

        private readonly TestCluster _cluster;

        public UnitGrainTest(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }

        [Fact]
        public async Task IsMessageCorrectAsync()
        {
            //set the message
            string message = "Test";

            var test = _cluster.GrainFactory.GetGrain<IUnitTestingGrain>(1);
            var result = await test.ReturnMessageForTestAsync(message);

            string expected = $"message: {message}";
            Assert.Equal(expected, result);
        }


        [Fact]
        public async Task TestGrainCommunication()
        {
            // instatiate the grain 
            var test = _cluster.GrainFactory.GetGrain<ICallingGrain>(Guid.Empty);

            // make calling grain 
            var result = await test.ReturnStringMessageAsync(1);

            // validate the resonse of the calling grain
            Assert.Equal("message: 2", result);
        }
    }
}

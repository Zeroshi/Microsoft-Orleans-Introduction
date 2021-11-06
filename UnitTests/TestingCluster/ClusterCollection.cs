using Orleans;
using Xunit;

namespace UnitTests
{
    [CollectionDefinition(ClusterCollection.Name)]
    public class ClusterCollection : ICollectionFixture<ClusterFixture>
    {
        public const string Name = "Chapter6UnitTesting";


    }
}

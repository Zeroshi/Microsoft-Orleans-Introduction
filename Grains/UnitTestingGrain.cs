using System.Threading.Tasks;

namespace Introduction
{
    public class UnitTestingGrain : Orleans.Grain, IUnitTestingGrain
    {
        public async Task<string> ReturnMessageForTestAsync(string message)
        {
            return await Task.FromResult($"message: {message}");
        }
    }
}

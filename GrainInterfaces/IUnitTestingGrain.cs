using System.Threading.Tasks;

namespace Introduction
{
    public interface IUnitTestingGrain : Orleans.IGrainWithIntegerKey
    {
        Task<string> ReturnMessageForTestAsync(string message);
    }
}

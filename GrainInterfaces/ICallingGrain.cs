using System.Threading.Tasks;

namespace Introduction
{
    public interface ICallingGrain : Orleans.IGrainWithGuidKey
    {
        Task<int> IncrementAsync(int number);
        Task<string> ReturnStringMessageAsync(int number);

    }
}

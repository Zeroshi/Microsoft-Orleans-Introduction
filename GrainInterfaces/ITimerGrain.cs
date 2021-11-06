using System.Threading.Tasks;

namespace Introduction
{
    public interface ITimerGrain : Orleans.IGrainWithGuidKey
    {
        void StartTimer();
        void StopTimer();
        Task<int> GetTimerNumberAsync();
    }
}

using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IReminderGrain : IGrainWithGuidKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}

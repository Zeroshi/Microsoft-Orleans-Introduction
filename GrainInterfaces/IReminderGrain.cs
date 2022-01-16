using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IReminderGrain : IGrainWithStringKey, IRemindable
    {
        Task SendMessage();
        Task StopMessage();
    }
}

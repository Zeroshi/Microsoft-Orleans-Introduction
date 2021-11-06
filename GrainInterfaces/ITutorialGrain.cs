using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Introduction
{
    public interface ITutorialGrain : Orleans.IGrainWithStringKey
    {
        Task<string> RespondWIthCurrentTime(string message);
    }
}

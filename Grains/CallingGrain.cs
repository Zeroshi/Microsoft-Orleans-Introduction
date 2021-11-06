using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Introduction
{
    public class CallingGrain : Orleans.Grain, ICallingGrain
    {
        private int latest = 0;

        //increment the given number
        public async Task<int> IncrementAsync(int number)
        {
            latest = number + 1;
            return await Task.FromResult(latest);
        }

        public async Task<string> ReturnStringMessageAsync(int number)
        {
            var grain = this.GrainFactory.GetGrain<IUnitTestingGrain>(1);
            return await grain.ReturnMessageForTestAsync(IncrementAsync(number).Result.ToString());
        }

        public new virtual IGrainFactory GrainFactory => base.GrainFactory;
        public virtual string GrainKey => this.GetPrimaryKeyString();
    }
}


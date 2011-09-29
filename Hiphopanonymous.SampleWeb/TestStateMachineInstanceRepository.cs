using System;
using Automatonymous;
using Hiphop;

namespace Hiphopanonymous.SampleWeb
{
    public class TestStateMachineInstanceRepository :
        StateMachineInstanceRepository
    {
        public StateMachineInstance Find(int id)
        {
            return new Ticket()
                   {
                       OpenedAt =  DateTime.Now
                   };
        }
    }
}
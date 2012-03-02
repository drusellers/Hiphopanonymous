using System.Collections.Generic;
using FubuMVC.Core;
using Automatonymous;

namespace Hiphopanonymous.Actions
{
    public class StateMachineInstanceOptionsAction<TStateMachine, TStateMachineInstance>
        where TStateMachine : StateMachine, new()
        where TStateMachineInstance : State, new()
    {
        [JsonEndpoint]
        public InstanceOptionsResult Execute(InstanceOptionsRequest<TStateMachine> request)
        {
            var tm = new TStateMachine();
            var tmi = new TStateMachineInstance();
            
            var evts = tm.NextEvents(tmi);

            //how to get the url of each event

            return new InstanceOptionsResult()
                   {
                       CurrentState = null, //tmi.CurrentState,
                       NextEventsAvailable = evts
                   };
        }
    }

    public class InstanceOptionsRequest<TStateMachine> where TStateMachine : StateMachine, new()
    {
        public int Id { get; set; }
    }

    public class InstanceOptionsResult
    {
        public IEnumerable<Event> NextEventsAvailable { get; set; }

        public State CurrentState { get; set; }
    }
}
using System.Collections.Generic;
using FubuMVC.Core;
using Automatonymous;

namespace Hiphopanonymous.Actions
{
    public class StateMachineInstanceOptionsAction<TStateMachine>
        where TStateMachine : StateMachine, new()
    {
        private readonly StateMachineInstanceRepository _repository;

        public StateMachineInstanceOptionsAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        [JsonEndpoint]
        public InstanceOptionsResult Execute(InstanceOptionsRequest<TStateMachine> request)
        {
            var tm = new TStateMachine();
            var tmi = _repository.Find(request.Id);

            //get the state machine instance

            var state = tmi.CurrentState;
            
            var evts = tm.NextEvents(state);

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
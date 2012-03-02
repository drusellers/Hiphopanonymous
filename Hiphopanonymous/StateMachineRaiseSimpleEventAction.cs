using System;
using System.Linq;
using Automatonymous;
using FubuMVC.Core;

namespace Hiphopanonymous
{
    public class StateMachineRaiseSimpleEventAction<TStateMachine>
        where TStateMachine : StateMachine<StateMachineInstance>, new()
    {
        
        private StateMachineInstanceRepository _repository;

        public StateMachineRaiseSimpleEventAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        public EventResult Execute(RaiseSimpleEvent<TStateMachine> input)
        {
            var smi = _repository.Find(input.Id);
            var sm = Activator.CreateInstance<TStateMachine>();

            try
            {
                sm.RaiseEvent(smi, m => m.Events.Single(e => e.Name == input.EventName));
            }
            catch (Exception)
            {
                //return error code whatever
                throw;
            }

            return new EventResult();
        }
    }

    public class RaiseSimpleEvent<TStateMachine>
    {
        [RouteInput]
        public int Id { get; set; }
        [RouteInput]
        public string EventName { get; set; }
    }
}
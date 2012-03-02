using System.Linq;
using FubuMVC.Core;
using System;
using Automatonymous;

namespace Hiphopanonymous.Actions
{
    public class StateMachineRaiseDataEventAction<TStateMachine, TData>
        where TStateMachine : StateMachine<StateMachineInstance>, new()
        where TData :class
    {
        private StateMachineInstanceRepository _repository;

        public StateMachineRaiseDataEventAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        public EventResult Execute(RaiseDataEvent<TStateMachine, TData> input)
        {
            var smi = _repository.Find(input.Id);
            var sm = Activator.CreateInstance<TStateMachine>();

            try
            {
                sm.RaiseEvent(smi, m=> (Event<TData>)m.Events.Single(e=>e.Name==input.EventName), input.Payload);
            }
            catch (Exception)
            {
                //return error code whatever
                throw;
            }
            
            return new EventResult();
        }
    }


    public class RaiseDataEvent<TStateMachine, TData>
        where TStateMachine : StateMachine, new()
        where TData : class
    {
        [RouteInput]
        public int Id { get; set; }

        [RouteInput]
        public string EventName { get; set; }

        public TData Payload { get; set; }
    }

    public class EventResult
    {
        public bool Success { get; set; }
        public Uri[] NextEvents { get; set; }
    }
}
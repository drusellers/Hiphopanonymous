namespace Hiphopanonymous
{
    using System;
    using Automatonymous;


    public class StateMachineRaiseDataEventAction<TStateMachine, TData>
        where TStateMachine : StateMachine, new()
    {
        private StateMachineInstanceRepository _repository;

        public StateMachineRaiseDataEventAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        public EventResult Execute(RaiseDataEvent<TStateMachine, TData> input)
        {
            var smi = _repository.Find(input.Id);
            //get state machine
            //build event
            //raise event
            return new EventResult();
        }
    }


    public class RaiseDataEvent<TStateMachine, TData>
        where TStateMachine : StateMachine, new()
    {
        public int Id { get; set; }
        public string EventName { get; set; }

        public TData Payload { get; set; }
    }

    public class EventResult
    {
        public bool Success { get; set; }
        public Uri[] NextEvents { get; set; }
    }
}
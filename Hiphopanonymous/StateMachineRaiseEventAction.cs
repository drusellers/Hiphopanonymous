namespace Hiphopanonymous
{
    using System;
    using Automatonymous;

    public class StateMachineRaiseEventAction<TStateMachine, TEvent>
        where TStateMachine : StateMachine, new()
        where TEvent : Event
    {
        private StateMachineInstanceRepository _repository;

        public StateMachineRaiseEventAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        public EventResult Execute(RaiseEvent<TStateMachine, TEvent> input)
        {
            var smi = _repository.Find(input.Id);
            //get state machine
            //build event
            //raise event
            return new EventResult();
        }
    }


    public class RaiseEvent<TStateMachine, TEvent>
        where TStateMachine : StateMachine, new()
        where TEvent : Event
    {
        public int Id { get; set; }
        public string Payload { get; set; }
    }

    public class EventResult
    {
        public bool Success { get; set; }
        public Uri[] NextEvents { get; set; }
    }
}
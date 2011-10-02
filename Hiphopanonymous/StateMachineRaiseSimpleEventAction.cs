using System.Linq;
using Automatonymous;

namespace Hiphopanonymous
{
    public class StateMachineRaiseSimpleEventAction<TStateMachine>
        where TStateMachine : StateMachine, new()
    {
        
        private StateMachineInstanceRepository _repository;

        public StateMachineRaiseSimpleEventAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        public EventResult Execute(RaiseSimpleEvent<TStateMachine> input)
        {
            var smi = _repository.Find(input.Id);
            
            //get state machine
            //build event
            //raise event
            return new EventResult();
        }
    }

    public class RaiseSimpleEvent<TStateMachine>
    {
        public int Id { get; set; }
        public string EventName { get; set; }
    }
}
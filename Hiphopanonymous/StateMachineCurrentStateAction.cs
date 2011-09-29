using Automatonymous;
using FubuMVC.Core;

namespace Hiphop
{
    public class StateMachineCurrentStateAction<TStateMachine> where TStateMachine : StateMachine, new()
    {
        private StateMachineInstanceRepository _repository;

        public StateMachineCurrentStateAction(StateMachineInstanceRepository repository)
        {
            _repository = repository;
        }

        [JsonEndpoint]
        public CurrentStateResult Execute(GetCurrentState<TStateMachine> request)
        {
            var instance = _repository.Find(request.Id);
            
            return new CurrentStateResult()
                   {
                       Instance = instance
                   };
        }
    }

    public class GetCurrentState<TStateMachine> where TStateMachine : StateMachine, new()
    {
        [RouteInput]
        public int Id { get; set; }
    }

    public class CurrentStateResult
    {
        public StateMachineInstance Instance { get; set; }
    }


}
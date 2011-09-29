namespace Hiphopanonymous
{
    using System.Collections.Generic;
    using FubuMVC.Core;
    using Automatonymous;

    public class StateMachineInstanceOptionsAction<TStateMachine> where TStateMachine : StateMachine, new()
    {
        [JsonEndpoint]
        public InstanceOptionsResult Execute(InstanceOptionsRequest<TStateMachine> request)
        {
            var tm = new TicketStateMachine();
            var tmi = new Ticket();
            tm.RaiseEvent(tmi, tm.Open);

            var evts = tm.NextEvents(tmi);

            //how to get the url of each event

            return new InstanceOptionsResult()
                   {
                       CurrentState = tmi.CurrentState,
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
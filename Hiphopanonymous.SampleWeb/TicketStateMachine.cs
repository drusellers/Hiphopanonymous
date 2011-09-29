﻿using System;
using Automatonymous;

namespace Hiphopanonymous.SampleWeb
{
    public class TicketStateMachine :
        StateMachine<Ticket>
    {
        public TicketStateMachine()
        {
            Event(() => Open);
            Event(() => Close);

            State(() => Opened);
            State(() => Closed);
            State(() => Stalled);

            Initially(
                When(Open)
                .Then(ticket=>
                {
                    ticket.OpenedAt = DateTime.Now;
                    ticket.DoStuff();
                })
                .TransitionTo(Opened));



            Anytime(
                When(Close)
                .TransitionTo(Closed));
        }

        public Event Open { get; set; }
        public Event Close { get; set; }


        public State Stalled { get; set; }
        public State Opened { get; set; }
        public State Closed { get; set; }
    }

    public class Ticket : 
        StateMachineInstance
    {
        public DateTime? OpenedAt { get; set; }
        public State CurrentState { get; set; }

        public void DoStuff()
        {
            
        }
    }
}
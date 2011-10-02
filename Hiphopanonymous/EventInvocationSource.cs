namespace Hiphopanonymous
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Automatonymous;
    using FubuMVC.Core.Registration;
    using FubuMVC.Core.Registration.Nodes;

    public class StateMachineActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {

            //var tpes = types.TypesMatching(t => t.Closes(typeof (StateMachine<>)));
            foreach(var sm in new List<Type>{typeof(TicketStateMachine)})
            {
                //TODO: Where can I load things into the container - using ObjDef?
                var machine = (StateMachine)Activator.CreateInstance(sm);

                var t = typeof (StateMachineOptionsAction<>).MakeGenericType(sm);
                var ta =  new ActionCall(t, t.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));
                //can I build the route here?
                
                yield return ta;

                var tt = typeof(StateMachineInstanceOptionsAction<>).MakeGenericType(sm);
                yield return new ActionCall(tt, tt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                var ttt = typeof(StateMachineCurrentStateAction<>).MakeGenericType(sm);
                yield return new ActionCall(ttt, ttt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                foreach (var @event in machine.Events)
                {
                    //need to track the event name
                    //between here and the event url generation
                    //so I used a custom ActionCall extension
                    var eventName = @event.Name;
                    var et = typeof (StateMachineRaiseEventAction<,>).MakeGenericType(sm, @event.GetType());
                    var call = new EventActionCall(eventName, et, et.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));
                    yield return call;
                }
            }
        }
    }
}
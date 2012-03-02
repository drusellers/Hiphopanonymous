using Automatonymous.Impl;
using FubuCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using Automatonymous;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using Hiphopanonymous.Actions;

namespace Hiphopanonymous.Config
{
    public class StateMachineActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {

            var stateMachines = types.TypesMatching(t => t.Closes(typeof (StateMachine<>)));
            foreach(var sm in stateMachines)
            {
                //TODO: Where can I load things into the container - using ObjDef?
                var machine = (StateMachine)Activator.CreateInstance(sm);

                var t = typeof (StateMachineOptionsAction<>).MakeGenericType(sm);
                var ta =  new ActionCall(t, t.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));
                
                yield return ta;

                //need to fix this
                var tt = typeof(StateMachineInstanceOptionsAction<,>).MakeGenericType(sm,null);
                yield return new ActionCall(tt, tt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                var ttt = typeof(StateMachineCurrentStateAction<>).MakeGenericType(sm);
                yield return new ActionCall(ttt, ttt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                foreach (var @event in machine.Events)
                {
                    //need to track the event name
                    //between here and the event url generation
                    //so I used a custom ActionCall extension
                    var eventName = @event.Name;

                    var tttt = typeof (StateMachineRaiseSimpleEventAction<>).MakeGenericType(sm);
                    var eventType = @event.GetType();
                    if (eventType != typeof (SimpleEvent))
                    {
                        tttt = typeof (StateMachineRaiseDataEventAction<,>).MakeGenericType(sm, eventType);
                    }

                    var call = new EventActionCall(eventName, tttt,
                                                   tttt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));
                    yield return call;
                }

                yield break;
            }
        }
    }
}
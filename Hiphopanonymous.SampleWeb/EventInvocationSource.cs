﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Automatonymous;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using Hiphop;

namespace Hiphopanonymous.SampleWeb
{
    public class StateMachineRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {

            registry.Actions.FindWith<StateMachineActionSource>();
            registry.Routes.UrlPolicy<StateMachineUrlPolicy>();
        }
    }

    public class StateMachineActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            IEnumerable<Type> foundStateMachineTypes = new List<Type>()
                                                       {
                                                           typeof(TicketStateMachine)
                                                       };

            foreach(var sm in foundStateMachineTypes)
            {
                //TODO: Where can I load things into the container - using ObjDef?
                var machine = (StateMachine)Activator.CreateInstance(sm);

                

                var t = typeof (StateMachineOptionsAction<>).MakeGenericType(sm);
                yield return new ActionCall(t, t.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                var tt = typeof(StateMachineInstanceOptionsAction<>).MakeGenericType(sm);
                yield return new ActionCall(tt, tt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                var ttt = typeof(StateMachineCurrentStateAction<>).MakeGenericType(sm);
                yield return new ActionCall(ttt, ttt.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));

                foreach (var @event in machine.Events)
                {
                    //need to track the event name somehow
                    //between here and the event url generation
                    var eventName = @event.Name;
                    var et = typeof (StateMachineRaiseEventAction<,>).MakeGenericType(sm, @event.GetType());
                    var call = new ActionCall(et, et.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance));
                    yield return call;
                }
            }
        }
    }

    public class StateMachineUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            return call.HandlerType.Namespace.Contains("Hiphopanonymous");
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var route = call.ToRouteDefinition();

            var stateMachineType = call.HandlerType.GetGenericArguments()[0];
            var machineName = stateMachineType.Name.Replace("StateMachine","").ToLower();
            route.Prepend(machineName);

            if(call.HandlerType.Name.Contains("Options"))
            {
                route.ConstrainToHttpMethods("OPTIONS");

                if(call.HandlerType.Name.Contains("Instance"))
                {
                    route.Append("{id}");
                }
            }
            
            if(call.HandlerType.Name.Contains("CurrentState"))
            {
                route.ConstrainToHttpMethods("GET");
                route.Append("{id}");
            }

            //add id to  those that need it
            
            //add event name
            if(call.HandlerType.Closes(typeof(StateMachineRaiseEventAction<,>)))
            {
                //this is a fail
                var eventName = call.HandlerType.GetGenericArguments()[1].Name.ToLower();
                route.Append("{id}");
                route.Append(eventName);
                route.ConstrainToHttpMethods("POST");
            }

            return route;
        }
    }


}
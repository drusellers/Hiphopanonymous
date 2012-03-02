using System;
using System.Reflection;
using FubuMVC.Core.Registration.Nodes;

namespace Hiphopanonymous.Actions
{
    public class EventActionCall : ActionCall
    {

        public EventActionCall(string name, Type handlerType, MethodInfo method) : base(handlerType, method)
        {
            EventName = name;
        }

        public string EventName { get; private set; }
    }
}
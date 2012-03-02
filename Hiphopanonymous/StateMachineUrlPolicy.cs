using FubuCore;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace Hiphopanonymous
{
    public class StateMachineUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            return call.HandlerType.Namespace.Contains("Hiphop");
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var route = call.ToRouteDefinition();

            var stateMachineType = call.HandlerType.GetGenericArguments()[0];
            var machineName = stateMachineType.Name.Replace("StateMachine","").ToLower();
            route.Prepend(machineName);

            if(call.HandlerType.Name.Contains("Options"))
            {
                route.ConstrainToHttpMethods("GET");

                if(call.HandlerType.Name.Contains("Instance"))
                {
                    route.Append("{id}"); // how do I bind this to the input? 
                }
            }
            
            if(call.HandlerType.Name.Contains("CurrentState"))
            {
                route.ConstrainToHttpMethods("GET");
                route.Append("{id}");
            }
            
            //add event name
            if(call.HandlerType.Closes(typeof(StateMachineRaiseDataEventAction<,>)) || call.HandlerType.Closes(typeof(StateMachineRaiseSimpleEventAction<>)))
            {
                //this is a fail, this is the type name - not the name of the event which would be the name
                //of the property the type was derived from.
                var eventName = ((EventActionCall) call).EventName.ToLower();
                route.Append("{id}");
                route.Append(eventName);
                route.ConstrainToHttpMethods("POST");
            }

            return route;
        }
    }
}
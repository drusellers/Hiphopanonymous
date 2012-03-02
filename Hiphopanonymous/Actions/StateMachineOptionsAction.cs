using System.Collections.Generic;
using System.Linq;
using Automatonymous;
using FubuMVC.Core;
using FubuMVC.Core.Urls;

namespace Hiphopanonymous.Actions
{
    public class StateMachineOptionsAction<TStateMachine> 
        where TStateMachine : StateMachine, new()
    {
        private IUrlRegistry _urls;

        public StateMachineOptionsAction(IUrlRegistry urls)
        {
            _urls = urls;
        }

        [JsonEndpoint]
        public OptionsResult Execute(OptionsRequest request)
        {
            var tm = new TStateMachine();

            var evts = tm.Events.Select(e=>
            {
                //handle 
                var i = typeof (RaiseSimpleEvent<TStateMachine>);
                return new NextEvent()
                       {
                           EventName = e,
                           MediaType = "application/bullshit",
                           //Find the Url
                           //Url = _urls.UrlFor(Activator.CreateInstance(i))
                       };
            });

            
            return new OptionsResult
                   {
                       EventsAvailable = evts
                   };
        }
    }

    public class OptionsResult
    {
        public IEnumerable<NextEvent> EventsAvailable { get; set; }
    }

    public class NextEvent
    {
        public Event EventName { get; set; }
        public string Url { get; set; }
        public string MediaType { get; set; }
        //what other bullshit do they want?
    }



    public class OptionsRequest
    {

    }
}
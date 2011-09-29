
using System;
using System.Collections.Generic;
using Automatonymous;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using System.Linq;

namespace Hiphop
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
                var i = typeof (RaiseEvent<,>).MakeGenericType(typeof (TStateMachine), e.GetType());
                return new NextEvent()
                       {
                           EventName = e,
                           MediaType = "application/bullshit",
                           Url = _urls.UrlFor(Activator.CreateInstance(i))
                       };
            });

            
            //how to get the url of each event

            return new OptionsResult()
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
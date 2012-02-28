using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using Hiphopanonymous.SampleWeb.App_Start;
using StructureMap;

// You can remove the reference to WebActivator by calling the Start() method from your Global.asax Application_Start
[assembly: WebActivator.PreApplicationStartMethod(typeof(AppStartFubuMVC), "Start", Order=0)]

namespace Hiphopanonymous.SampleWeb.App_Start
{
    public static class AppStartFubuMVC
    {
        public static void Start()
        {
            // FubuApplication "guides" the bootstrapping of the FubuMVC
            // application
            var container = new Container(cfg=>
            {
                cfg.For<StateMachineInstanceRepository>().Use<TestStateMachineInstanceRepository>();
            });

            FubuApplication.For<ConfigureFubuMVC>() // ConfigureFubuMVC is the main FubuRegistry
                                                    // for this application.  FubuRegistry classes 
                                                    // are used to register conventions, policies,
                                                    // and various other parts of a FubuMVC application


                // FubuMVC requires an IoC container for its own internals.
                // In this case, we're using a brand new StructureMap container,
                // but FubuMVC just adds configuration to an IoC container so
                // that you can use the native registration API's for your
                // IoC container for the rest of your application
                .StructureMap(container)
                .Bootstrap();
        }
    }
}
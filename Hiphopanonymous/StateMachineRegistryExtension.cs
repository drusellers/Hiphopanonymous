using FubuMVC.Core;
using Hiphopanonymous.Config;

namespace Hiphopanonymous
{
    public class StateMachineRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Actions.FindWith<StateMachineActionSource>();
            registry.Routes.UrlPolicy<StateMachineUrlPolicy>();
        }
    }
}
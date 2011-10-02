using FubuMVC.Core;

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
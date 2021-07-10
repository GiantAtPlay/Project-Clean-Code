using LightInject;

namespace Application.IoC
{
    public class HangfireIoC
    {
        //[HF]: Manage the dependencies needed for background tasks here.
        public static ServiceContainer BackgroundTaskServiceContainer()
        {
            var serviceContainer = new ServiceContainer();
            //[LI]: In this instance we are registering everything from the composition root. You may
            //      decide you only wish to register a subset needed by the background tasks.
            serviceContainer.RegisterFrom<CompositionRoot>();
            
            return serviceContainer;
        }
    }
}
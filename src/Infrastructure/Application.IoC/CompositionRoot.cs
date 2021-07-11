using Application.Core;
using Application.Data;
using Application.Data.Movies;
using Application.DatabaseUpdate;
using Application.Domain;
using Application.Domain.Data;
using Application.Domain.Emails;
using Application.Domain.Movies;
using Application.Email;
using Application.Hangfire;
using LightInject;
using Microsoft.Extensions.Configuration;

namespace Application.IoC
{
    public class CompositionRoot : ICompositionRoot
    {
        // [LI]: This example registers everything in a single class. To ease the maintenance you may
        //       prefer to to split this class into multiple. All composition should still be done in this project.
        public void Compose(IServiceRegistry serviceRegistry)
        {
            // [LI]: Register infrastructure services.
            serviceRegistry.RegisterSingleton<IBackgroundTaskService, BackgroundTaskService>();
            serviceRegistry.RegisterSingleton<IEmailService, EmailService>();
            serviceRegistry.RegisterSingleton<IApplicationClock, ApplicationClock>();
            
            // [DB]: Register database updater in IoC
            serviceRegistry.RegisterSingleton<IDatabaseUpdater, DatabaseUpdater>();
            
            // [DATA]: Register data repositories
            serviceRegistry.Register(typeof(IRepository<IMovie>), typeof(MovieRepository));
        }
    }
}
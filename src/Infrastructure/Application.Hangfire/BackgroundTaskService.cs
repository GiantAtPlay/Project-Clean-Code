using System;
using System.Linq.Expressions;
using Application.Domain;
using Hangfire;

namespace Application.Hangfire
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public BackgroundTaskService() : this(new BackgroundJobClient(), new RecurringJobManager()) {}

        public BackgroundTaskService(IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        public string Run(Expression<Action> methodCall)
            => _backgroundJobClient.Enqueue(methodCall);
        
        public string Run<T>(Expression<Action<T>> methodCall) 
            => _backgroundJobClient.Enqueue(methodCall);
        
        public string Schedule<T>(Expression<Action<T>> task, int minutes) 
            => _backgroundJobClient.Schedule(task, TimeSpan.FromMinutes(minutes));
        
        public string Schedule<T>(Expression<Action<T>> methodCall, DateTime dateTime) 
            => _backgroundJobClient.Schedule(methodCall, new DateTimeOffset(dateTime));

        public void RunDaily<T>(string recurringJobId, Expression<Action<T>> methodCall, int hour, int minutes)
        {
            var cronExpression = Cron.Daily(hour, minutes);
            var localTimeZone = TimeZoneInfo.Local;
            _recurringJobManager.AddOrUpdate(recurringJobId, methodCall, cronExpression, localTimeZone);
        }
    }
}
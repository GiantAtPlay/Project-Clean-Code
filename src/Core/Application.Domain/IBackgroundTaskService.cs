using System;
using System.Linq.Expressions;

namespace Application.Domain
{
    public interface IBackgroundTaskService
    {
        string Run(Expression<Action> methodCall);
        string Run<T>(Expression<Action<T>> methodCall);

        string Schedule<T>(Expression<Action<T>> methodCall, int delay);

        string Schedule<T>(Expression<Action<T>> methodCall, DateTime dateTime);
        
        void RunDaily<T>(string recurringJobId, Expression<Action<T>> methodCall, int hour, int minutes);
    }
}
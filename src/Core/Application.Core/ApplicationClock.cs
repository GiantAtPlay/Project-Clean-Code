using System;
using Application.Domain;

namespace Application.Core
{
    public class ApplicationClock : IApplicationClock
    {
        //Project: Standardising the use of datetime within the application.
        public DateTime Now => DateTime.UtcNow;
    }
}
using System;

namespace Application.Domain
{
    public interface IApplicationClock
    {
        DateTime Now { get; }
    }
}
using System;

namespace Application.Domain.Reports
{
    public interface IReport
    {
        int Id { get; set; }
        ReportType Type { get; set; }
        ReportStatus Status { get; set; }
        DateTime DateRequested { get; set; }
        DateTime? DateProcessed { get; set; }
    }
}
using System;

namespace Application.Domain.Reports
{
    public interface IReportRequest
    {
        ReportType ReportType { get; }
    }
}
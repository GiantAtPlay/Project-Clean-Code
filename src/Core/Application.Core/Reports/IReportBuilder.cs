using Application.Domain.Reports;

namespace Application.Core.Reports
{
    public interface IReportBuilder
    {
        ReportType Type { get; }
        byte[] Build(IReport request);
    }
}
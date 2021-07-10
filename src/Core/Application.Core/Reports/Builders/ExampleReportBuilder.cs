using Application.Domain.Reports;

namespace Application.Core.Reports.Builders
{
    public class ExampleReportBuilder : IReportBuilder
    {
        public ReportType Type => ReportType.ExampleReport;
        
        public byte[] Build(IReport request)
        {
            throw new System.NotImplementedException();
        }
    }
}
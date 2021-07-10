namespace Application.Domain.Reports
{
    public interface IReportService
    {
        string Process(IReport request);
        void Request(IReportRequest request);
        string RequestAndProcess(IReportRequest request);
    }
}
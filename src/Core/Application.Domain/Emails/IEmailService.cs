namespace Application.Domain.Emails
{
    public interface IEmailService
    {
        void Send(IEmail email);
    }
}
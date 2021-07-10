namespace Application.Domain.Emails
{
    public interface IEmail
    {
        IRecipient Recipient { get; }
        string Subject { get; }
        string Body();
        bool IsHtml { get; }
    }
}
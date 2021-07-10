namespace Application.Domain.Emails
{
    public interface IRecipient
    {
        string Salutation { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
    }
}
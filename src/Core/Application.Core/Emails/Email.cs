using System;
using Application.Domain.Emails;

namespace Application.Core.Emails
{
    public abstract class Email : IEmail
    {
        public IRecipient Recipient { get; }
        public virtual string Subject => throw new NotImplementedException();
        public virtual bool IsHtml => true;
        
        //Project: Ensuring we always have a recipient when we attempt to send an email.
        protected Email(IRecipient recipient)
        {
            if (string.IsNullOrWhiteSpace(recipient.EmailAddress))
                throw new ArgumentException("Emails address is required to send an email.");
            
            Recipient = recipient;
        }

        public abstract string Body();
    }
}
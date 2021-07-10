using Application.Domain.Emails;

namespace Application.Core.Emails
{
    public class ExampleEmail : Email
    {
        //Project: Not setting the subject will result in an error.
        public override string Subject => "Subject of my example email.";
        
        //Project: Most emails we send are html, feel free to reverse the default in 'Email.cs' as needed.
        public override bool IsHtml => false;

        private readonly string _stringNeededByEmail;
        
        public ExampleEmail(IRecipient recipient, string stringNeededByEmail) : base(recipient)
        {
            _stringNeededByEmail = stringNeededByEmail;
        }
        
        //Project: This is where the content of the email is generated.
        public override string Body()
        {
            return $"This is an example email. The string from the constructor was: '{_stringNeededByEmail}'.";
        }
    }
}
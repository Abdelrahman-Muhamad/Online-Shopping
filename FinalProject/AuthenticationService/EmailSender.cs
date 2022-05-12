using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinalProject.AuthenticationService
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.
        public Task SendEmailAsync(string toEmail, string subject, string message)
        {
            return Execute(Options.SendGridKey,subject,message,toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
        
            var client = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("gehadk.bakry@gmail.com", "Email Confirm"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            //return client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
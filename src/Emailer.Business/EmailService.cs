using System.Reflection;
using Emailer.Business.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Emailer.Business;
public class EmailService : IEmailService
{
    ISendGridClient _client;
    
    public EmailService(ISendGridClient client)
    {
        _client = client;
    }
    
    public async Task SendEmail()
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress("example@email.com", "Test"),
            Subject = "Sending with Twilio SendGrid is Fun"
        };
        msg.AddContent(MimeType.Html, GetHtml());
        msg.AddTo(new EmailAddress("example@email.com", "Test"));
        var response = await _client.SendEmailAsync(msg).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("it failed");
        }
    }

    string GetHtml()
    {
        var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(directoryName,"templates", "ConfirmationEmail.html");
        var content = File.ReadAllText(path);
        return content;
    }
}
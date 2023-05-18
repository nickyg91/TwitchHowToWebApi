

using System.Net.Mail;
using Orchard.Core.Configuration;
using Orchard.Core.Exceptions;

namespace Orchard.Core.Services.Email;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly string _environmentUrl;

    public EmailService(SmtpSettings smtpSettings, string environmentUrl)
    {
        _smtpSettings = smtpSettings;
        _environmentUrl = environmentUrl;
    }

    public async Task SendAccountVerificationEmail(Guid verificationGuid, string to)
    {
        try
        {
            var body = 
                @$"
                Please click the following link to verify your Orchard account!
                {_environmentUrl}/account/verify/{verificationGuid.ToString()}
                This URL will expire within 3 days of your account creation.
                To request another one, please log in and request verification.
            ";
            var email = new MailMessage("noreply@nickganter.dev", to, "Verify your Orchard Account!" ,body);
            
            using var smtpClient = new SmtpClient(_smtpSettings.SmtpAddress, _smtpSettings.Port);
            await smtpClient.SendMailAsync(email);
        }
        catch (Exception e)
        {
            throw new EmailSendFailedException("Failed to send account verification email.", e);
        }
    }
}
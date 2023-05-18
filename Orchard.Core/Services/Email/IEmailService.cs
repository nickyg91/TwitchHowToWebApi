using System.Net.Mail;

namespace Orchard.Core.Services.Email;

public interface IEmailService
{
    Task SendAccountVerificationEmail(Guid verificationGuid, string to);
}
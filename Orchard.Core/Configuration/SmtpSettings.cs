namespace Orchard.Core.Configuration;

public class SmtpSettings
{
    public const string SmtpSettingsKey = "SmtpSettings";
    public string SmtpAddress { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
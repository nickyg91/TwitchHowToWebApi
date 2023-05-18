using Orchard.Core.Configuration;

namespace Orchard.Web.Configuration;

public class OrchardConfiguration
{
    public JwtSettings? JwtSettings { get; set; }
    public RedisSettings? RedisSettings { get; set; }
    public SmtpSettings? SmtpSettings { get; set; }
}
namespace Orchard.Core.Configuration;

public class RedisSettings
{
    public const string RedisSettingsSection = "Redis";
    public string ConnectionString { get; set; }
    public int MaxRetries { get; set; }
}
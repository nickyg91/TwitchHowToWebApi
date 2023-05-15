using StackExchange.Redis;

namespace Orchard.Data.Cache;

public interface ICacheService
{
    ConnectionMultiplexer Connect(int numberOfRetries);
    IDatabase Database { get; }
    Task<T?> ReadObject<T>(string key);
    Task WriteObject<T>(string key, T obj);

}
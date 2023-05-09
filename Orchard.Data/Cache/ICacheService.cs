using StackExchange.Redis;

namespace Orchard.Data.Cache;

public interface ICacheService
{
    public ConnectionMultiplexer Connect(int numberOfRetries);
    public IDatabase Database { get; }
}
using Microsoft.Extensions.Options;
using Orchard.Core.Configuration;
using StackExchange.Redis;

namespace Orchard.Data.Cache;

public class CacheService : ICacheService
{
    private Lazy<ConnectionMultiplexer> _redisConnection { get; }
    private readonly string _connectionString;
    private readonly int _maxRetries;
    public IDatabase Database => _redisConnection.Value.GetDatabase();

    public CacheService(IOptions<RedisSettings> settings)
    {
        _connectionString = settings.Value.ConnectionString;
        _maxRetries = settings.Value.MaxRetries;
        _redisConnection = new Lazy<ConnectionMultiplexer>(() => Connect(0));
    }

    public ConnectionMultiplexer Connect(int numberOfRetries = 0)
    {
        ConnectionMultiplexer muxer;
        try
        {
            muxer = ConnectionMultiplexer.Connect(_connectionString);
        }
        catch (RedisConnectionException e)
        {
            Console.WriteLine(e);
            if (numberOfRetries < _maxRetries)
            {
                return Connect(numberOfRetries++);
            }
            throw;
        }
        return muxer;
    }
}
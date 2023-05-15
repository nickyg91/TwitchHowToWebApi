using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Orchard.Core.Configuration;
using Orchard.Core.Exceptions;
using StackExchange.Redis;

namespace Orchard.Data.Cache;

public class CacheService : ICacheService
{
    private readonly Lazy<ConnectionMultiplexer> _redisConnection;
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
    
    public async Task<T?> ReadObject<T>(string key)
    {
        string? item = await Database.StringGetAsync(key);

        if (string.IsNullOrEmpty(item))
        {
            throw new CachedItemNotFoundException($"Cannot find item for key {key}.");
        }

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(item));
        var result = await JsonSerializer.DeserializeAsync<T>(stream);
        return result;
    }

    public async Task WriteObject<T>(string key, T obj)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, obj);
        var json = Encoding.UTF8.GetString(stream.GetBuffer());
        await Database.StringSetAsync(key, json);
    }
}
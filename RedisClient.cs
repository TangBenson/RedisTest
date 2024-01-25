using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisTest;

public sealed class RedisClient
{
    // private static readonly Lazy<RedisClient> lazy = new(() => new RedisClient());
    private static readonly Lazy<RedisClient> lazy = new(() =>
    {
        if (string.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
        return new RedisClient();
    });

    private static string? _settingOption;
    public readonly ConnectionMultiplexer ConnectionMultiplexer;

    public static RedisClient Instance { get { return lazy.Value; } }

    private RedisClient()
    {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption!);
    }

    public static void Init(string settingOption)
    {
        _settingOption = settingOption;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisTest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TryController : ControllerBase
    {
        // private readonly RedisClient _redisClient;
        private readonly RedisClient2 _redisClient2;
        public TryController(
            // RedisClient redisClient,
            RedisClient2 redisClient2
            )
        {
            // _redisClient = redisClient;
            _redisClient2 = redisClient2;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GoodSet(string id)
        {
            // var db = _redisClient.ConnectionMultiplexer.GetDatabase(0);
            var db = _redisClient2.Database;
            var key = $"test{id}";
            var value = await db.StringGetAsync(key);
            if (value.HasValue)
            {
                return Ok(value);
            }
            else
            {
                await db.StringSetAsync(key, $"{key}@");
                return Ok("test");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BadSet(string id)
        {
            //最簡單建立Redis連線的方式，但是每次都會建立新的連線，效能不好
            var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            var db = redis.GetDatabase();
            var key = $"test{id}";
            var value = await db.StringGetAsync(key);
            if (value.HasValue)
            {
                return Ok(value);
            }
            else
            {
                await db.StringSetAsync(key, $"{key}@");
                return Ok("test");
            }
        }
    }
}
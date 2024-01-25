using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _redisDb;
        public TryController(IConfiguration configuration, RedisClient redisClient)
        {
            _configuration = configuration;
            RedisClient.Init(_configuration["Redis"]!);
            _redisDb = redisClient.Database;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var key = "test";
            var value = await _redisDb.StringGetAsync(key);
            if (value.HasValue)
            {
                return Ok(value);
            }
            else
            {
                await _redisDb.StringSetAsync(key, "test");
                return Ok("test");
            }
        }
    }
}
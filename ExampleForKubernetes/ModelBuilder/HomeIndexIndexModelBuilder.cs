using System;
using ExampleForKubernetes.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace ExampleForKubernetes.ModelBuilder
{
    public class HomeIndexIndexModelBuilder : IHomeIndexModelBuilder
    {
        private readonly IDistributedCache _cache;

        public HomeIndexIndexModelBuilder(IDistributedCache cache)
        {
            _cache = cache;
        }

        public HomeIndexModel GetHomeIndexModel()
        {
            return new HomeIndexModel
            {
                RedisCounter = GetRedisCounter(),
            };
        }

        private int GetRedisCounter()
        {
            // Quick example of DistributedCache usage
            const string cacheKey = "mykey";
            var value = _cache.GetString(cacheKey);
            var counter = string.IsNullOrWhiteSpace(value) ? 1 : Convert.ToInt32(value) + 1;
            _cache.SetString(cacheKey, counter.ToString());
            return counter;
        }
    }
}
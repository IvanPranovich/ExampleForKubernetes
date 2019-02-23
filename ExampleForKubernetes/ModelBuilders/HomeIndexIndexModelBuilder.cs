using System;
using System.Linq;
using ExampleForKubernetes.Data;
using ExampleForKubernetes.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace ExampleForKubernetes.ModelBuilders
{
    public class HomeIndexIndexModelBuilder : IHomeIndexModelBuilder
    {
        private readonly IDistributedCache _cache;
        private readonly SampleContext _dbContext;

        public HomeIndexIndexModelBuilder(IDistributedCache cache, SampleContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public HomeIndexModel GetHomeIndexModel()
        {
            var cities = _dbContext.Cities.Select(c => $"{c.Name} ({c.Country})").ToList();
            return new HomeIndexModel
            {
                RedisCounter = GetRedisCounter(),
                Cities = cities,
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
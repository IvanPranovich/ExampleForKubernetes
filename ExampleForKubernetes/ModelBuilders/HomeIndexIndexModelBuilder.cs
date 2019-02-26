using System;
using System.Collections.Generic;
using System.Linq;
using ExampleForKubernetes.Data;
using ExampleForKubernetes.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace ExampleForKubernetes.ModelBuilders
{
    public class HomeIndexIndexModelBuilder : IHomeIndexModelBuilder
    {
        private readonly IDistributedCache _cache;
        private readonly SampleContext _dbContext;
        private readonly IConfiguration _configuration;

        public HomeIndexIndexModelBuilder(IDistributedCache cache, SampleContext dbContext, IConfiguration configuration)
        {
            _cache = cache;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public HomeIndexModel GetHomeIndexModel()
        {
            return new HomeIndexModel
            {
                RedisCounter = GetRedisCounter(),
                Cities = GetCities(),
                Config = GetConfiguration(),
            };
        }

        private string GetConfiguration()
        {
            return $"Redis: {_configuration.GetConnectionString("RedisConnection")}{Environment.NewLine}MySql: {_configuration.GetConnectionString("SqlServerConnection")}";
        }

        private List<string> GetCities()
        {
            try
            {
                return _dbContext.Cities.Select(c => $"{c.Name} ({c.Country})").ToList();
            }
            catch (Exception e)
            {
                // Quick plug
                return null;
            }
        }

        private int GetRedisCounter()
        {
            try
            {
                // Quick example of DistributedCache usage
                const string cacheKey = "mykey";
                var value = _cache.GetString(cacheKey);
                var counter = string.IsNullOrWhiteSpace(value) ? 1 : Convert.ToInt32(value) + 1;
                _cache.SetString(cacheKey, counter.ToString());
                return counter;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
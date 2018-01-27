using DesafioStone.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.IO;
using Xunit;

namespace DesafioStone.Tests
{
    public class TestApiItems : IDisposable
    {
        IConfiguration Config;

        public TestApiItems()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json");
            Config = configurationBuilder.Build();

            MongoDbContext.ConnectionString = Config.GetSection("MongoConnection:ConnectionString").Value;
            MongoDbContext.DatabaseName = Config.GetSection("MongoConnection:DatabaseName").Value;
        }

        public void Dispose()
        {
            //MongoDbContext dbContext = new MongoDbContext();
            //dbContext.Items.DeleteMany(FilterDefinition<Item>.Empty);
        }

        [Fact]
        public void TestPostCreateItemShouldCountMore1InDb()
        {
            MongoDbContext dbContext = new MongoDbContext();
            Assert.True(true);
        }
    }
}

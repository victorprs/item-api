using DesafioStone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace DesafioStone.Tests
{
    public class TestApiItems : IDisposable
    {
        IConfiguration Config;
        private TestServer testServer;
        private HttpClient client;

        Item item1;
        Item item2;
        Item item3;

        public TestApiItems()
        {
            item1 = new Item
            {
                Codigo = 1,
                Andar = 8,
                Descricao = "teste",
                Livre = true,
                CriadoEm = new DateTime(2018,01,01).ToUniversalTime(),
                AtualizadoEm = new DateTime(2018, 01, 01).ToUniversalTime()
            };
            item2 = new Item
            {
                Codigo = 2,
                Andar = 7,
                Descricao = "outro",
                Livre = true,
                CriadoEm = new DateTime(2018, 01, 01),
                AtualizadoEm = new DateTime(2018, 01, 01)
            };
            item3 = new Item
            {
                Codigo = 3,
                Andar = 7,
                Descricao = "outros",
                Livre = false,
                CriadoEm = new DateTime(2018, 01, 01),
                AtualizadoEm = new DateTime(2018, 01, 01)
            };

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json");
            Config = configurationBuilder.Build();

            MongoDbContext.ConnectionString = Config.GetSection("MongoConnection:ConnectionString").Value;
            MongoDbContext.DatabaseName = Config.GetSection("MongoConnection:DatabaseName").Value;


            MongoDbContext dbContext = new MongoDbContext();
            dbContext.Items.InsertOne(item1);
            dbContext.Items.InsertOne(item3);

            Startup.AppSettingsFileName = "testsettings.json";
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            client = testServer.CreateClient();
        }

        public void Dispose()
        {
            MongoDbContext dbContext = new MongoDbContext();
            dbContext.Items.DeleteMany(FilterDefinition<Item>.Empty);
            testServer.Dispose();
            client.Dispose();
        }

        [Fact]
        public async void TestPostCreateItemShouldCountMore1InDb()
        {
            MongoDbContext dbContext = new MongoDbContext();

            long previousCount = dbContext.Items.Count(FilterDefinition<Item>.Empty);

            var stringContent = new StringContent(JsonConvert.SerializeObject(item2), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/items", stringContent);
            Assert.Equal(previousCount + 1, dbContext.Items.Count(FilterDefinition<Item>.Empty));
        }

        [Fact]
        public async void TestGetItemsShouldReturnOk()
        {
            var response = await client.GetAsync("api/items");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void TestGetItem1ShouldReturnItem1()
        {
            var response = await client.GetAsync("api/items/1");
            var result = JsonConvert.DeserializeObject<Item>(await response.Content.ReadAsStringAsync());
            Assert.Equal(item1.Codigo, result.Codigo);
            Assert.Equal(item1.Andar, result.Andar);
            Assert.Equal(item1.Descricao, result.Descricao);
            Assert.Equal(item1.AtualizadoEm, result.AtualizadoEm);
            Assert.Equal(item1.CriadoEm, result.CriadoEm);
            Assert.Equal(item1.Livre, result.Livre);
        }

        [Fact]
        public async void TestDeleteShouldRemove1InDb()
        {
            MongoDbContext dbContext = new MongoDbContext();

            long previousCount = dbContext.Items.Count(FilterDefinition<Item>.Empty);
            
            var response = await client.DeleteAsync("api/items/3");
            Assert.Equal(previousCount - 1, dbContext.Items.Count(FilterDefinition<Item>.Empty));
        }
    }
}

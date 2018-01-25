using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioStone.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioStone.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public List<Item> Get()
        {
            MongoDbContext dbContext = new MongoDbContext();
            return dbContext.Items.Find(new BsonDocument()).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

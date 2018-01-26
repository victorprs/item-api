using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioStone.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioStone.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            MongoDbContext dbContext = new MongoDbContext();
            try
            {
                return Ok(dbContext.Items.Find(new BsonDocument()).ToList());
            }
            catch (InvalidOperationException)
            {
                return NotFound(new JsonResult("No items found"));
            }
        }

        // GET api/<controller>/5
        [HttpGet("{codigo}")]
        public IActionResult Get(int codigo)
        {
            MongoDbContext dbContext = new MongoDbContext();
            try
            {
                return Ok(dbContext.Items.Find(x => x.Codigo == codigo).Single());
            }
            catch (InvalidOperationException)
            {
                return NotFound(new JsonResult(String.Format("No item with codigo = {0} was found", codigo)));
            }
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

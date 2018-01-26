using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioStone.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Http;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioStone.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery]bool livre, [FromQuery]int andar, [FromQuery]String descricao)
        {
            MongoDbContext dbContext = new MongoDbContext();


            var builder = Builders<Item>.Filter;
            var livreFilter = builder.Eq(x => x.Livre, livre);
            var andarFilter = builder.Eq(x => x.Andar, andar);
            var descricaoFilter = builder.Eq(x => x.Descricao, descricao);
            FilterDefinition<Item> query = builder.Empty;

            if (Request.Query.ContainsKey("livre"))
                query = query & livreFilter;
            if (Request.Query.ContainsKey("andar"))
                query = query & andarFilter;
            if (Request.Query.ContainsKey("descricao"))
                query = query & descricaoFilter;
            try
            {
                if (Request.Query.Count == 0)
                    return Ok(dbContext.Items.Find(new BsonDocument()).ToList());
                else
                    return Ok(dbContext.Items.Find(query).ToList());
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
        public IActionResult Post([FromBody]Item item)
        {
            MongoDbContext dbContext = new MongoDbContext();

            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                dbContext.Items.InsertOne(item);
                return Ok(new { Location = HttpContext.Request.Host + "/api/items/" + item.Codigo});
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{codigo}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            MongoDbContext dbContext = new MongoDbContext();
            dbContext.Items.DeleteOne(x => x.Codigo == codigo);

            return NoContent();
        }
    }
}

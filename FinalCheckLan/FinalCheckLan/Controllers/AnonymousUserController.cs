using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCheckLan.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCheckLan.Controllers
{

    [Route("api/[controller]")]
    public class AnonymousUserController : Controller
    {
        static List<MovieItem> movieItemsList = new List<MovieItem>();
        static AnonymousUserController()
        {
            movieItemsList.Add(new MovieItem { Id = 501, Name = "The silent voice", DateOfLaunch = DateTime.Parse("10/05/1998"), Genre = "anime" });
            movieItemsList.Add(new MovieItem { Id = 502, Name = "summer wars", DateOfLaunch = DateTime.Parse("10/05/1999"), Genre = "anime romcom" });
            movieItemsList.Add(new MovieItem { Id = 503, Name = "akira", DateOfLaunch = DateTime.Parse("10/05/2000"), Genre = "anime" });
            movieItemsList.Add(new MovieItem { Id = 504, Name = "your name", DateOfLaunch = DateTime.Parse("10/05/2001"), Genre = "rom com" });

        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movieItemsList);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieItem movieItem)
        {
            var it = movieItemsList.Find(m => m.Id == movieItem.Id);
            if (it == null)
            {
                return new BadRequestObjectResult("Invalid Movie id");
            }
            else
            {
                it.Name = movieItem.Name;
                it.Genre = movieItem.Genre;
                it.DateOfLaunch = movieItem.DateOfLaunch;
            }
            return Ok(it);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

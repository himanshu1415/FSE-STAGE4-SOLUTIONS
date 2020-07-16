using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeCheckLan.Models;



namespace PracticeCheckLan.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        static List<MenuItem> mItems = new List<MenuItem>();
        static AdminController()
        {
            mItems.Add(new MenuItem { Id=1,Name = "Ratatouille", Price = 99.00, Active = true, DateOfLaunch = DateTime.Parse("10/05/1998"), Category = "Main Course", FreeDelivery = true });
            mItems.Add(new MenuItem { Id=2,Name = "sizzler", Price = 99.00, Active = true, DateOfLaunch = DateTime.Parse("10/05/1998"), Category = "Main Course", FreeDelivery = true });
            
            mItems.Add(new MenuItem { Id=4,Name = "rabdi", Price = 99.00, Active = true, DateOfLaunch = DateTime.Parse("10/05/1998"), Category = "Dessert", FreeDelivery = true });
            mItems.Add(new MenuItem { Id=5,Name = "paneertikka", Price = 99.00, Active = true, DateOfLaunch = DateTime.Parse("10/05/1998"), Category = "Starter", FreeDelivery = true });
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(mItems);
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
        public IActionResult Put(int id, [FromBody]MenuItem menuItem)
        {
            var it = mItems.Find(m=>m.Id==menuItem.Id);
            if(it==null)
            {
                return new BadRequestObjectResult("Invalid item id");
            }
            else
            {
                it.Name = menuItem.Name;
                it.Price = menuItem.Price;
                it.Active = menuItem.Active;
                it.Category = menuItem.Category;
                it.DateOfLaunch = menuItem.DateOfLaunch;
                it.FreeDelivery = menuItem.FreeDelivery;
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

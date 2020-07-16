using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeCheckLan.Models;



namespace PracticeCheckLan.Controllers
{
    [Route("api/[controller]")]
    public class NewCustomerController : Controller
    {
        List<Cart> carts = new List<Cart>();
        public NewCustomerController()
        {
            carts.Add(new Cart { CartId = 1, MenuItemId = 2 });
            carts.Add(new Cart { CartId = 2, MenuItemId = 1 });
            carts.Add(new Cart { CartId = 3, MenuItemId = 3 });

        }
        private readonly MenuItemOperation operation;

        public NewCustomerController(MenuItemOperation operation)
        {
            this.operation = operation;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<MenuItem> its = await operation.GetMenuItems();
            List<MenuItem> retlist = new List<MenuItem>();
            foreach (var i in its)
            {
                if(i.Active && i.DateOfLaunch<DateTime.Now)
                {
                    retlist.Add(i);
                }
            }
            return Ok(retlist);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            List<MenuItem> it =  await operation.GetMenuItems();
            double price = 0;
            string items = "Items in Cart are ";
            foreach (var item in carts)
            {
                foreach (var m in it)
                {
                    if (item.MenuItemId == m.Id)
                    {
                        price += m.Price;
                        items += m.Name + "  ";
                    }

                }
            }
            items += "\n Total price is "+price.ToString();
            return Ok(items);
        }

        // GET api/<controller>/5
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(MenuItem m)
        {
            Cart c = new Cart();
            c.CartId = 1;
            c.MenuItemId = m.Id;
            int rows = operation.AddNew(m);
            if (rows > 0)
                return new StatusCodeResult(201);
            else
                return new StatusCodeResult(500);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(MenuItem p)
        {

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            id = 1;
            int x=carts.FindIndex(c => c.CartId == id);
            if(x!=0)
            {
                carts.RemoveAt(x);

            }
            else
            {
                return new BadRequestObjectResult("invalid cart item");
            }

            return new StatusCodeResult(500);
        }
    }
}

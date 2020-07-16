using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCheckLan.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCheckLan.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        List<Cart> carts = new List<Cart>();
        public CartController()
        {
            carts.Add(new Cart { CartId = 1, MovieItemId = 502 });
            carts.Add(new Cart { CartId = 2, MovieItemId = 501 });
            carts.Add(new Cart { CartId = 3, MovieItemId = 503 });

        }
        private readonly MovieItemOperation operation;

        public CartController(MovieItemOperation operation)
        {
            this.operation = operation;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<MovieItem> its = await operation.GetMovieItems();
            List<MovieItem> retlist = new List<MovieItem>();
            foreach (var it in its)
            {
                if (it.DateOfLaunch < DateTime.Now)
                {
                    retlist.Add(it);
                }
            }
            return Ok(retlist);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            List<MovieItem> it = await operation.GetMovieItems();
            string its = "Items in Cart are ";
            foreach (var item in carts)
            {
                foreach (var m in it)
                {
                    if (item.MovieItemId == m.Id)
                    {
                        its += m.Name + "  ";
                    }

                }
            }
            return Ok(its);
        }

        // GET api/<controller>/5
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(MovieItem m)
        {
            Cart c = new Cart();
            c.CartId = 1;
            c.MovieItemId = m.Id;
            int rows = operation.AddNew(m);
            if (rows > 0)
                return new StatusCodeResult(201);
            else
                return new StatusCodeResult(500);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            id = 1;
            int x = carts.FindIndex(c => c.CartId == id);
            if (x != 0)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Bangazon.Models;
using Bangazon.Data;

namespace BangazonAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]

    public class OrdersController : Controller
    {
        private BangazonContext context;

        public OrdersController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet] // square brackets are decorations / anotations
        // IActionResult is a default type 
        public IActionResult Get()   
        {
            IQueryable<object> orders = from order in context.Order select order;

            if (orders == null)
            {
                return NotFound();  // creates a 404 response that is already built
            }

            return Ok(orders);   // creates a 200 response that is already built in and returns JSON file of "orders" 
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.OrderId = id;
            context.Order.Update(order);
            try
            {
               context.SaveChanges();

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id, [FromBody] Order order)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.OrderId = id;
            context.Order.Remove(order);
            try
            {
               context.SaveChanges();

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }
         private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }
    }
}

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

    public class ProductsController : Controller
    {
        private BangazonContext context;

        public ProductsController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet] // square brackets are decorations / anotations
        // IActionResult is a default type 
        public IActionResult Get()   
        {
            IQueryable<object> products = from product in context.Product select product;

            if (products == null)
            {
                return NotFound();  // creates a 404 response that is already built
            }

            return Ok(products);   // creates a 200 response that is already built in and returns JSON file of "products" 

        }
        // GET api/values/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product product = context.Product.Single(m => m.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Product.Add(product);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.ProductId = id;
            context.Product.Update(product);
            try
            {
               context.SaveChanges();

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.ProductId = id;
            context.Product.Remove(product);
            try
            {
               context.SaveChanges();

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }
         private bool ProductExists(int id)
        {
            return context.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}

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

    public class LineItemController : Controller
    {
        private BangazonContext context;

        public LineItemController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet] // square brackets are decorations / anotations
        // IActionResult is a default type 
        public IActionResult Get()   
        {
            IQueryable<object> lineitems = from lineitem in context.LineItem select lineitem;

            if (lineitems == null)
            {
                return NotFound();  // creates a 404 response that is already built
            }

            return Ok(lineitems);   // creates a 200 response that is already built in and returns JSON file of "products" 

        }
        // GET api/values/5
        [HttpGet("{id}", Name = "getItems")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineitem == null)
                {
                    return NotFound();
                }
                
                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] LineItem lineitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.LineItem.Add(lineitem);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineitem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("getItems", new { id = lineitem.LineItemId }, lineitem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody]LineItem lineitem)
        {
            if (id != lineitem.LineItemId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            context.LineItem.Update(lineitem);
            try
            {
                context.SaveChanges();

                if (lineitem == null)
                {
                    return NotFound();
                }
                
                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);
            
            if (lineitem == null)
            {
                return NotFound();
            }

            try
            {
                context.LineItem.Remove(lineitem);
                context.SaveChanges();
                return Ok(lineitem);
            }
            
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

         private bool LineItemExists(int id)
        {
            return context.LineItem.Count(e => e.LineItemId == id) > 0;
        }
    }
}

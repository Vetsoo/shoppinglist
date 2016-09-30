using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShoppingList.Models;
using ShoppingList.Models.DTO;

namespace ShoppingList.Controllers
{
    public class ShoppingListsController : ApiController
    {
        private ShoppingListContext db = new ShoppingListContext();

        // GET: api/ShoppingLists
        public IQueryable<ShoppingListDTO> GetShoppingLists()
        {
            var shoppingLists = from s in db.ShoppingLists
                           select new ShoppingListDTO()
                           {
                               Id = s.Id,
                               Name = s.Name
                           };

            return shoppingLists;
        }

        // GET: api/ShoppingLists/5
        [ResponseType(typeof(Models.ShoppingList))]
        public async Task<IHttpActionResult> GetShoppingList(int id)
        {
            Models.ShoppingList shoppingList = await db.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return Ok(shoppingList);
        }

        // PUT: api/ShoppingLists/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShoppingList(int id, Models.ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingList.Id)
            {
                return BadRequest();
            }

            db.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShoppingLists
        [ResponseType(typeof(Models.ShoppingList))]
        public async Task<IHttpActionResult> PostShoppingList(Models.ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingLists.Add(shoppingList);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = shoppingList.Id }, shoppingList);
        }

        // DELETE: api/ShoppingLists/5
        [ResponseType(typeof(Models.ShoppingList))]
        public async Task<IHttpActionResult> DeleteShoppingList(int id)
        {
            Models.ShoppingList shoppingList = await db.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            db.ShoppingLists.Remove(shoppingList);
            await db.SaveChangesAsync();

            return Ok(shoppingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingListExists(int id)
        {
            return db.ShoppingLists.Count(e => e.Id == id) > 0;
        }
    }
}
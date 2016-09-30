﻿using System;
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
    public class ProductsController : ApiController
    {
        private readonly ShoppingListContext db = new ShoppingListContext();

        // GET: api/Products
        public IQueryable<ProductDTO> GetProducts()
        {
            var products = from p in db.Products
                           select new ProductDTO()
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Brand = p.Brand,
                               Description = p.Description,
                               Image = p.Image
                           };

            return products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await db.Products.Select(p =>
            new ProductDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Description = p.Description,
                Image = p.Image
            }).SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            // New code:

            var dto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Description = product.Description,
                Image = product.Image
            };

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, dto);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}
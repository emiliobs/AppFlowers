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
using FlowersBack.Models;

namespace FlowersBack.Controllers.API
{
    public class FlowersController : ApiController
    {
        private DataContext db;

        public FlowersController()
        {
            db = new DataContext();
        }

        // GET: api/Flowers
        public IQueryable<Flower> GetFlowers()
        {
            return db.Flowers;
        }

        // GET: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public async Task<IHttpActionResult> GetFlower(int id)
        {
            Flower flower = await db.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        // PUT: api/Flowers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFlower(int id, Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flower.FlowerId)
            {
                return BadRequest();
            }

            db.Entry(flower).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(id))
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

        // POST: api/Flowers
        [ResponseType(typeof(Flower))]
        public async Task<IHttpActionResult> PostFlower(Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flowers.Add(flower);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = flower.FlowerId }, flower);
        }

        // DELETE: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public async Task<IHttpActionResult> DeleteFlower(int id)
        {
            Flower flower = await db.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            db.Flowers.Remove(flower);
            await db.SaveChangesAsync();

            return Ok(flower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlowerExists(int id)
        {
            return db.Flowers.Count(e => e.FlowerId == id) > 0;
        }
    }
}
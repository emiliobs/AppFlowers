using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlowersBack.Models;

namespace FlowersBack.Controllers.API
{
    public class FlowersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Flowers
        public IQueryable<Flower> GetFlowers()
        {
            return db.Flowers;
        }

        // GET: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public IHttpActionResult GetFlower(int id)
        {
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        // PUT: api/Flowers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlower(int id, Flower flower)
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
                db.SaveChanges();
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
        public IHttpActionResult PostFlower(Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flowers.Add(flower);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flower.FlowerId }, flower);
        }

        // DELETE: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public IHttpActionResult DeleteFlower(int id)
        {
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }

            db.Flowers.Remove(flower);
            db.SaveChanges();

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
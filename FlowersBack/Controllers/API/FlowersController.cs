using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FlowersBack.Models;
using FlowersBack.Classes;

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
        public async Task<IHttpActionResult> PutFlower(int id, FlowersRequest flowersRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flowersRequest.FlowerId)
            {
                return BadRequest();
            }

            if (flowersRequest.ImageArray != null && flowersRequest.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(flowersRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Images";
                var fullpath = $"{folder}/{file}";
                var response = Fileshelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    flowersRequest.Image = fullpath;
                }

            }

            var flower = ToFlower(flowersRequest);
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
        public async Task<IHttpActionResult> PostFlower(FlowersRequest flowersRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (flowersRequest.ImageArray != null && flowersRequest.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(flowersRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Images";
                var fullpath = $"{folder}/{file}";
                var response = Fileshelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    flowersRequest.Image = fullpath;
                }

            }

            var flower = ToFlower(flowersRequest);

            db.Flowers.Add(flower);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = flower.FlowerId }, flower);
        }

        private Flower ToFlower(FlowersRequest flowersRequest)
        {
             return new Flower()
             {
                 Description = flowersRequest.Description,
                 Image = flowersRequest.Image,
                 FlowerId = flowersRequest.FlowerId,
                 IsActive = flowersRequest.IsActive,
                 LastPurchase = flowersRequest.LastPurchase,
                 Observation = flowersRequest.Observation,
                 Price = flowersRequest.Price,
             };
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
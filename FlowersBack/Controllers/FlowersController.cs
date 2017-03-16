using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FlowersBack.Models;
using FlowersBack.Classes;

namespace FlowersBack.Controllers
{
    public class FlowersController : Controller
    {
        private DataContext db;

        public FlowersController()
        {
            db = new DataContext();
        }

        // GET: Flowers
        public ActionResult Index()
        {
            return View(db.Flowers.OrderBy(f => f.Description).ToList());
        }

        // GET: Flowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flower flower = db.Flowers.Find(id);

            if (flower == null)
            {
                return HttpNotFound();
            }

            return View(flower);
        }

        [HttpGet]
        // GET: Flowers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flowers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlowerView flowerView)
        {
            if (ModelState.IsValid)
            {

                var pic = string.Empty;
                var folder = "~/Images";

                if (flowerView.ImageFile != null)
                {
                    pic = Fileshelper.UploadPhoto(flowerView.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }


                //Método
                var flower = ToFlower(flowerView);
                flower.Image = pic;

                db.Flowers.Add(flower);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(flowerView);
        }

        private Flower ToFlower(FlowerView flowerView)
        {
            return   new Flower()
            {
                Description = flowerView.Description,
                FlowerId =  flowerView.FlowerId,
                Image =  flowerView.Image,
                IsActive = flowerView.IsActive,
                LastPurchase = flowerView.LastPurchase,
                Observation = flowerView.Observation,
                Price = flowerView.Price,


            };
        }

        // GET: Flowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Flower flower = db.Flowers.Find(id);

            if (flower == null)
            {
                return HttpNotFound();
            }

            return View(ToView(flower));
        }

        private FlowerView ToView(Flower view)
        {
            return new FlowerView()
            {
                Description = view.Description,
                FlowerId = view.FlowerId,
                Image = view.Image,
                IsActive = view.IsActive,
                LastPurchase = view.LastPurchase,
                Observation = view.Observation,
                Price = view.Price,


            };
        }

        // POST: Flowers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FlowerView flowerView)
        {
            if (ModelState.IsValid)
            {
                var picture = flowerView.Image;
                var folder = "~/Images";

                if (flowerView.ImageFile != null)
                {
                    picture = Fileshelper.UploadPhoto(flowerView.ImageFile, folder);
                    picture = $"{folder}/{picture}";
                }

                var flower = ToFlower(flowerView);

                flower.Image = picture;

                db.Entry(flower).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(flowerView);
        }

        // GET: Flowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = db.Flowers.Find(id);

            if (flower == null)
            {
                return HttpNotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flower flower = db.Flowers.Find(id);

            db.Flowers.Remove(flower);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

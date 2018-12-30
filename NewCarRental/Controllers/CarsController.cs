using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewCarRental.Helpers;
using NewCarRental.Models.DAL;

namespace NewCarRental.Controllers
{
    public class CarsController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();       
        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.CarType);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            cars.Photo = StaticGlobalHelpers.ImageToString(cars.Photo);
            return View(cars);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.CarTypeId = new SelectList(db.CarType, "Id", "Type");
            return View();
        }

        // POST: Cars/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cars cars)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images/"), fileName);                    
                    file.SaveAs(path);
                    cars.Photo = path;
                }
                db.Cars.Add(cars);
                db.SaveChanges();
             
                return RedirectToAction("Index");
            }

            ViewBag.CarTypeId = new SelectList(db.CarType, "Id", "Type", cars.CarTypeId);
            return View(cars);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarTypeId = new SelectList(db.CarType, "Id", "Type", cars.CarTypeId);
            cars.Photo = StaticGlobalHelpers.ImageToString(cars.Photo);
            return View(cars);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarTypeId,Name,Brand,Model,ProductionYear,Stock")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Images/"), fileName);
                    file.SaveAs(path);
                    cars.Photo = path;
                }
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();             
                return RedirectToAction("Index");
            }
            ViewBag.CarTypeId = new SelectList(db.CarType, "Id", "Type", cars.CarTypeId);
            return View(cars);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cars cars = db.Cars.Find(id);
            db.Cars.Remove(cars);
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


        public ActionResult UploadDocument()
        {
            return View();
        }
    }
}


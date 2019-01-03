using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewCarRental.Helpers;
using NewCarRental.Models.DAL;

namespace NewCarRental.Controllers
{   
    public class ReservationsController : Controller
    {
        private CarRentalEntities db = new CarRentalEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Cars).Include(r => r.Customers);
            return View(reservations.ToList());
        }
      
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // GET: Reservations/Create

        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers.Where(a => a.Id == UserHelper.User.Id), "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
       
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarId,CustomerId,DateFrom,DateTo")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                //sprawdza czy wystarczająca ilość aut
                var stock = db.Cars.Where(a => a.Id == reservations.CarId).Select(a => a.Stock).FirstOrDefault();
                var currentReservations = db.Reservations.Where(
               a => a.CarId == reservations.CarId && (
                a.DateFrom >= reservations.DateFrom && a.DateFrom < reservations.DateTo
                || a.DateTo > reservations.DateFrom && a.DateTo <= reservations.DateTo
                || a.DateFrom <= reservations.DateFrom && a.DateTo >= reservations.DateTo))
                .ToList();
                if (stock <= currentReservations.Count())
                {
                    ViewBag.Message = "Brak dostępnych samochodów w tym terminie.";
                    ViewBag.CarId = new SelectList(db.Cars, "Id", "Name", reservations.CarId);
                    ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", reservations.CustomerId);
                    return View(reservations);
                }
                db.Reservations.Add(reservations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Name", reservations.CarId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", reservations.CustomerId);
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Name", reservations.CarId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", reservations.CustomerId);
            return View(reservations);
        }

        // POST: Reservations/Edit/5
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarId,CustomerId,DateFrom,DateTo")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Name", reservations.CarId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", reservations.CustomerId);
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            db.Reservations.Remove(reservations);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCContext db = new FortuneTellerMVCContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstName = customer.FirstName;
            ViewBag.LastName = customer.LastName;

            //age determines how many years to retirement
            if (customer.Age % 2 == 0)
            {
                ViewBag.Retire = "20";
            }
            else
            { 
                ViewBag.Retire = "10";
            }

            #region Birth month and account balance
            if (customer.BirthMonth <= 0)
            {
                ViewBag.Balance = "$0.00";
            }
            else if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.Balance = "$25,000";
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                ViewBag.Balance = "$250,000";
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                ViewBag.Balance = "$1 million";
            }
            else
            {
                ViewBag.Balance = "$0.00";
            }
            #endregion

            #region number of siblings
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.VacHome = "Aspen";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.VacHome = "Montana";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.VacHome = "Naples, FL";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.VacHome = "Paris";
            }
            else if (customer.NumberOfSiblings > 3)
            {
                ViewBag.VacHome = "Rehobeth Beach";
            }
            else
            {
                ViewBag.VacHome = "Detroit";
            }
            #endregion

            #region Favorite Color
            if (customer.FavoriteColor == "red")
            {
                ViewBag.FavColor = "a Bentley.";
            }
            else if (customer.FavoriteColor == "orange")
            {
                ViewBag.FavColor = "a yacht.";
            }
            else if (customer.FavoriteColor == "yellow")
            {
                ViewBag.FavColor = "a helicopter.";
            }
            else if (customer.FavoriteColor == "green")
            {
                ViewBag.FavColor = "a Lear jet.";
            }
            else if (customer.FavoriteColor == "blue")
            {
                ViewBag.FavColor = "a dinghy.";
            }
            else if (customer.FavoriteColor == "indigo")
            {
                ViewBag.FavColor = "a truck.";
            }
            else
            {
                Console.Write("a limousine.");
            }
            #endregion

            return View(customer);
            }//end Details Method

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

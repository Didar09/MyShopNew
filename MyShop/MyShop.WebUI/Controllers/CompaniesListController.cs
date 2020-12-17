using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CompaniesListController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CompaniesList
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.Countries).Include(c => c.States).OrderByDescending(x=>x.CreatedAt);
            return View(companies.ToList());
        }

        // GET: CompaniesList/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: CompaniesList/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName", company.CountryId);
            ViewBag.StatesId = new SelectList(db.States, "Id", "State", company.StatesId);
            return View(company);
        }

        // POST: CompaniesList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,ContactPersonName,ContactPersonPMobile,ContactPersonEmail,CompanyAddress,CompanyPhone,Password,Roles,IsActive,StatesId,CountryId,CreatedAt")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "CountryName", company.CountryId);
            ViewBag.StatesId = new SelectList(db.States, "Id", "State", company.StatesId);
            return View(company);
        }

        // GET: CompaniesList/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: CompaniesList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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

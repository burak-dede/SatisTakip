using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SatisTakip.Models;
using SatisTakip.DAL;
using PagedList;

namespace SatisTakip.Controllers
{
    public class TurkcellSaleController : Controller
    {
        private SaleContext db = new SaleContext();


        // GET: /TurkcellSale/

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? tabIndex)
        {

            @ViewBag.Title = "Turkcell";
            @ViewBag.BodyLogoUrl = "/Content/Images/turkcelllogo.png";

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewBag.NameSortParm = (sortOrder == "name" || sortOrder == "name_desc") ? (sortOrder == "name_desc" ? "name" : "name_desc") : "name";
            ViewBag.ActiveDateparm = (sortOrder == "actdate" || sortOrder == "actdate_desc") ? (sortOrder == "actdate_desc" ? "actdate" : "actdate_desc") : "actdate_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            TurkcellListsModel lists = new TurkcellListsModel();

            int pageSize = 20;

            lists.SearchResults1Page = 1;
            lists.SearchResults2Page = 1;
            lists.SearchResults3Page = 1;

            ViewBag.tabindex = tabIndex;

            switch (tabIndex)
            {
                case 1:
                    lists.SearchResults1Page = (int)page;
                    break;
                case 2:
                    lists.SearchResults2Page = (int)page;
                    break;
                case 3:
                    lists.SearchResults3Page = (int)page;
                    break;

            }


            IQueryable<TurkcellSale> sales;
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = db.TurkcellSales.Where(s => s.Name.Contains(searchString));
            }
            else
            {
                sales = from s in db.TurkcellSales select s;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sales = sales.OrderByDescending(s => s.Name);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "name":
                    sales = sales.OrderBy(s => s.Name);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "actdate":
                    sales = sales.OrderBy(s => s.ActivationDate);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "actdate_desc":
                    sales = sales.OrderByDescending(s => s.ActivationDate);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                default:
                    sales = sales.OrderByDescending(s => s.ActivationDate);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
            }


            IQueryable<TurkcellSale> Faturali = sales.Where(s => s.LineType.Equals(true) && s.CustomerState.Equals(true));
            IQueryable<TurkcellSale> Faturasiz = sales.Where(s => s.LineType.Equals(false) && s.CustomerState.Equals(true));

            lists.SearchResults2 = Faturali.ToPagedList(lists.SearchResults2Page, pageSize);
            lists.SearchResults3 = Faturasiz.ToPagedList(lists.SearchResults3Page, pageSize);

            return View(lists);

        }

        // GET: /TurkcellSale/Details/5

        [Authorize]
        public ActionResult Details(int? id)
        {
            
            @ViewBag.Title = "Turkcell";
            @ViewBag.BodyLogoUrl = "/Content/Images/turkcelllogo.png";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurkcellSale turkcellsale = db.TurkcellSales.Find(id);
            if (turkcellsale == null)
            {
                return HttpNotFound();
            }
            return View(turkcellsale);
        }

        [Authorize]
        // GET: /TurkcellSale/Create
        public ActionResult Create()
        {
            @ViewBag.Title = "Turkcell";
            @ViewBag.BodyLogoUrl = "/Content/Images/turkcelllogo.png";

            return View();
        }

        // POST: /TurkcellSale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include="Id,Name,Lastname,PhoneNumber,LineType,CustomerType,ActivationDate,ContactNumber,Note,CustomerState")] TurkcellSale turkcellsale)
        {
            if (ModelState.IsValid)
            {
                db.TurkcellSales.Add(turkcellsale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turkcellsale);
        }

        [Authorize]
        // GET: /TurkcellSale/Edit/5
        public ActionResult Edit(int? id)
        {
            @ViewBag.Title = "Turkcell";
            @ViewBag.BodyLogoUrl = "/Content/Images/turkcelllogo.png";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurkcellSale turkcellsale = db.TurkcellSales.Find(id);
            if (turkcellsale == null)
            {
                return HttpNotFound();
            }
            return View(turkcellsale);
        }

        // POST: /TurkcellSale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include="Id,Name,Lastname,PhoneNumber,LineType,CustomerType,ActivationDate,ContactNumber,Note,CustomerState")] TurkcellSale turkcellsale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turkcellsale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turkcellsale);
        }

        // GET: /TurkcellSale/Delete/5

        [Authorize]
        public ActionResult Delete(int? id)
        {
            @ViewBag.Title = "Turkcell";
            @ViewBag.BodyLogoUrl = "/Content/Images/turkcelllogo.png";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TurkcellSale turkcellsale = db.TurkcellSales.Find(id);
            if (turkcellsale == null)
            {
                return HttpNotFound();
            }
            return View(turkcellsale);
        }

        // POST: /TurkcellSale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            TurkcellSale turkcellsale = db.TurkcellSales.Find(id);
            db.TurkcellSales.Remove(turkcellsale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SatisTakip.Models;
using SatisTakip.DAL;
using PagedList;

namespace SatisTakip.Controllers
{
    public class CompanyTwoSaleController : Controller
    {
        private SaleContext db = new SaleContext();


        // GET: /CompanyTwoSale/

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? tabIndex)
        {

            @ViewBag.Title = "CompanyTwo";
            @ViewBag.BodyLogoUrl = "/Content/Images/CompanyTwologo.png";

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

            CompanyTwoListsModel lists = new CompanyTwoListsModel();

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


            IQueryable<CompanyTwoSale> sales;
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = db.CompanyTwoSales.Where(s => s.Name.Contains(searchString));
            }
            else
            {
                sales = from s in db.CompanyTwoSales select s;
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


            IQueryable<CompanyTwoSale> Faturali = sales.Where(s => s.LineType.Equals(true) && s.CustomerState.Equals(true));
            IQueryable<CompanyTwoSale> Faturasiz = sales.Where(s => s.LineType.Equals(false) && s.CustomerState.Equals(true));

            lists.SearchResults2 = Faturali.ToPagedList(lists.SearchResults2Page, pageSize);
            lists.SearchResults3 = Faturasiz.ToPagedList(lists.SearchResults3Page, pageSize);

            return View(lists);

        }

        // GET: /CompanyTwoSale/Details/5

        [Authorize]
        public ActionResult Details(int? id)
        {
            
            @ViewBag.Title = "CompanyTwo";
            @ViewBag.BodyLogoUrl = "/Content/Images/CompanyTwologo.png";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyTwoSale CompanyTwosale = db.CompanyTwoSales.Find(id);
            if (CompanyTwosale == null)
            {
                return HttpNotFound();
            }
            return View(CompanyTwosale);
        }

        [Authorize]
        // GET: /CompanyTwoSale/Create
        public ActionResult Create()
        {
            @ViewBag.Title = "CompanyTwo";
            @ViewBag.BodyLogoUrl = "/Content/Images/CompanyTwologo.png";

            return View();
        }

        // POST: /CompanyTwoSale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include="Id,Name,Lastname,PhoneNumber,LineType,CustomerType,ActivationDate,ContactNumber,Note,CustomerState")] CompanyTwoSale CompanyTwosale)
        {
            if (ModelState.IsValid)
            {
                db.CompanyTwoSales.Add(CompanyTwosale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CompanyTwosale);
        }

        [Authorize]
        // GET: /CompanyTwoSale/Edit/5
        public ActionResult Edit(int? id)
        {
            @ViewBag.Title = "CompanyTwo";
            @ViewBag.BodyLogoUrl = "/Content/Images/CompanyTwologo.png";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyTwoSale CompanyTwosale = db.CompanyTwoSales.Find(id);
            if (CompanyTwosale == null)
            {
                return HttpNotFound();
            }
            return View(CompanyTwosale);
        }

        // POST: /CompanyTwoSale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include="Id,Name,Lastname,PhoneNumber,LineType,CustomerType,ActivationDate,ContactNumber,Note,CustomerState")] CompanyTwoSale CompanyTwosale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(CompanyTwosale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CompanyTwosale);
        }

        // GET: /CompanyTwoSale/Delete/5

        [Authorize]
        public ActionResult Delete(int? id)
        {
            @ViewBag.Title = "CompanyTwo";
            @ViewBag.BodyLogoUrl = "/Content/Images/CompanyTwologo.png";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyTwoSale CompanyTwosale = db.CompanyTwoSales.Find(id);
            if (CompanyTwosale == null)
            {
                return HttpNotFound();
            }
            return View(CompanyTwosale);
        }

        // POST: /CompanyTwoSale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyTwoSale CompanyTwosale = db.CompanyTwoSales.Find(id);
            db.CompanyTwoSales.Remove(CompanyTwosale);
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

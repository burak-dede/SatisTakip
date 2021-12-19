using System;
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
    public class CompanyOneSaleController : Controller
    {

        private SaleContext db = new SaleContext();

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? tabIndex)
        {
            @ViewBag.Title = "CompanyOne";


            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewBag.NameSortParm = (sortOrder == "name" || sortOrder == "name_desc") ? (sortOrder == "name_desc" ? "name" : "name_desc") : "name";
            ViewBag.SaleDateSortParm = (sortOrder == "saledate" || sortOrder == "saledate_desc") ? (sortOrder == "saledate_desc" ? "saledate" : "saledate_desc") : "saledate_desc";
            ViewBag.EndDateSortParm = (sortOrder == "endDate" || sortOrder == "endDate_desc") ? (sortOrder == "endDate_desc" ? "endDate" : "endDate_desc") : "endDate_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            ListsModel lists = new ListsModel();

            int pageSize = 20;

            lists.SearchResults1Page = 1;
            lists.SearchResults2Page = 1;
            lists.SearchResults3Page = 1;
            lists.SearchResults4Page = 1;

            ViewBag.tabindex = tabIndex;

            switch(tabIndex)
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
                case 4:
                    lists.SearchResults4Page = (int)page;
                    break;

            }


            IQueryable<CompanyOneSale> sales;
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = db.Sales.Where(s => s.CompanyName.Contains(searchString));
            }
            else
            {
                sales = from s in db.Sales select s;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sales = sales.OrderByDescending(s => s.CompanyName);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "name":
                    sales = sales.OrderBy(s => s.CompanyName);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "saledate":
                    sales = sales.OrderBy(s => s.DateofSale);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "saledate_desc":
                    sales = sales.OrderByDescending(s => s.DateofSale);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "endDate":
                    sales = sales.OrderBy(s => s.EndOfContractDate);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                case "endDate_desc":
                    sales = sales.OrderByDescending(s => s.EndOfContractDate);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
                default:
                    sales = sales.OrderBy(s => s.CompanyName);
                    lists.SearchResults1 = sales.ToPagedList(lists.SearchResults1Page, pageSize);
                    break;
            }



            IQueryable<CompanyOneSale> saleDevam = sales.Where(s => s.CustomerState.Equals(true));

            DateTime thisDate = DateTime.Today;
            IQueryable<CompanyOneSale> saleYakin = sales.Where(s => (DbFunctions.DiffDays(DateTime.Today, s.EndOfContractDate) <= 30) && (DbFunctions.DiffDays(DateTime.Today, s.EndOfContractDate) > 0));
            IQueryable<CompanyOneSale> saleBiten = sales.Where(s => s.CustomerState.Equals(false));

            lists.SearchResults2 = saleDevam.ToPagedList(lists.SearchResults2Page, pageSize);
            lists.SearchResults3 = saleYakin.ToPagedList(lists.SearchResults3Page, pageSize);
            lists.SearchResults4 = saleBiten.ToPagedList(lists.SearchResults4Page, pageSize);


            ViewBag.BodyLogoUrl = "/Content/Images/CompanyOnelogo.png";


            return View(lists);

        }

        // GET: /Sale/Details/5

        [Authorize]
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "CompanyOne";
            ViewBag.Category = "Detay";
            ViewBag.BodyLogoUrl = "/Content/Images/CompanyOnelogo.png";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyOneSale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: /Sale/Create

        [Authorize]
        public ActionResult Create()
        {

            ViewBag.Title = "CompanyOne";
            ViewBag.Category = "Yeni";
            ViewBag.BodyLogoUrl = "../../Content/Images/CompanyOnelogo.png";
            //ViewBag.selectMobileList = getMobileList();

            return View();
        }

        // POST: /Sale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,CompanyName,Sales,DateofSale,EndOfContractDate,IsMobileDataOwn,MobileDate,RemindingTime")] CompanyOneSale sale, HttpPostedFileBase file)
        {

            String fileName = "";
            string physicalPath = "";
            if (ModelState.IsValid)
            {  
                if (file != null)
                    {
                    try
                    {

                            fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                            physicalPath = Server.MapPath("~/Content/Images/Invoices/" + sale.CompanyName + "_" + fileName);
                            // save image in folder
                            file.SaveAs(physicalPath);
                    sale.InvoiceImagePath = "~/Content/Images/Invoices/" + sale.CompanyName + "_" + fileName;
                    }
                    catch(Exception e)
                    {
                        ModelState.AddModelError("uploadError", e);
                    }
                }
            
                try
                {
                    db.Sales.Add(sale);
                    db.SaveChanges();
                }
                catch
                {
                    sale.InvoiceImagePath = "~/Content/Images/Invoices/" + sale.CompanyName + "_" + fileName;
                    sale.MobileDate = sale.DateofSale;
                    db.Sales.Add(sale);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            return View(sale);
        }

        // GET: /Sale/Edit/5

        [Authorize]
        public ActionResult Edit(int? id)
        {

            ViewBag.Title = "CompanyOne";
            ViewBag.Category = "Düzenle";
            ViewBag.BodyLogoUrl = "../../Content/Images/CompanyOnelogo.png";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyOneSale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: /Sale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,Sales,DateofSale,EndOfContractDate,IsMobileDataOwn,MobileDate,RemindingTime,CustomerState,InvoiceImagePath")] CompanyOneSale sale, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                
                String fileName = "";
                string physicalPath = "";

                if (file!=null)
                {

                    fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                    physicalPath = Server.MapPath("~/Content/Images/Invoices/" + sale.CompanyName + "_" + fileName);
                    // save image in folder
                    file.SaveAs(physicalPath);
                    sale.InvoiceImagePath = "~/Content/Images/Invoices/" + sale.CompanyName + "_" + fileName;
                }

                try
                {

                    db.Entry(sale).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    sale.MobileDate = sale.DateofSale;
                    db.Entry(sale).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(sale);
        }

        // GET: /Sale/Delete/5

        [Authorize]
        public ActionResult Delete(int? id)
        {

            ViewBag.Title = "CompanyOne";
            ViewBag.Category = "Sil";
            ViewBag.BodyLogoUrl = "../../Content/Images/CompanyOnelogo.png";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyOneSale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: /Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyOneSale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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

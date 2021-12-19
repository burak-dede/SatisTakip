using System.Web.Mvc;
namespace SatisTakip.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Anasayfa";
            return View();
        }
    }
    /*
    public class Job
    {

        private SaleContext db = new SaleContext();


        public async void Announcement()
        {
            try
            {
                DateTime lastMailDate = db.MailLogs.OrderByDescending(p => p.MailDate).Take(1).Select(x => x.MailDate).ToList().LastOrDefault();

                if (lastMailDate != null)
                {
                    DateTime dnow = DateTime.Now;
                    double diffDate = (dnow - lastMailDate).TotalDays;
                    if (diffDate <= 1)
                    {
                        return;
                    }

                }
            }
            catch
            {

            }

            logMail newMailLog = new logMail();
            string mailmessage="";

            IQueryable<CompanyOne> sales;
            sales = from s in db.Sales select s;

            IQueryable<CompanyOne> saleYakin = sales.Where(s => (DbFunctions.DiffDays(DateTime.Today, s.EndOfContractDate) <= 7) && (DbFunctions.DiffDays(DateTime.Today, s.EndOfContractDate) > 0) && s.CustomerState == true);
            List<CompanyOne> listYakin = saleYakin.ToList<CompanyOne>();
            IQueryable<CompanyOne> saleBiten = sales.Where(s => (DbFunctions.DiffDays(DateTime.Today, s.EndOfContractDate) <= 0) && s.CustomerState == true);
            List<CompanyOne> listBiten = saleBiten.ToList<CompanyOne>();

            foreach (CompanyOne item in listBiten)
            {
                item.CustomerState = false;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }


            if (listYakin.Count > 0 || listBiten.Count > 0)
            {
                var body = "";

                mailmessage = mailmessage + "BITECEKLER: ";
                if (listYakin.Count > 0)
                {

                    body = body + "<h3 style='color: white; background-color: #5e97f2'>Sürelerinin dolmasına 1 hafta kalan şirketler ve şahıslar aşağıda listenlenmiştir;</h3></br><table class='table'><tr style='color: #5e97f2'><th scope='col'>Şirket Adı</th>" +
                        "<th scope='col'>Satış Miktarı</th><th scope='col'>Başlama Tarihi</th><th scope='col'>Bitiş Tarihi</th><th scope='col'>Mobil Hat Sahibi</th><th scope='col'>Mobil Hat Tarihi</th></tr>";
                    foreach (var item in listYakin)
                    {
                        body = body +
                            "<tr><td style='font-weight:bold'>" + item.CompanyName + "</td><td style='text-align: center'>" + item.Sales + "</td><td>" + item.DateofSale + "</td><td>" + item.EndOfContractDate + "</td><td style='text-align: center'>";
                        if (item.IsMobileDataOwn == true)
                        {
                            body = body + "EVET</td><td>" + item.MobileDate + "</td></tr>";
                        }
                        else
                        {
                            body = body + "HAYIR</td><td>" + item.MobileDate + "</td></tr>";
                        }
                        mailmessage = mailmessage + item.CompanyName+", ";
                    }
                    body = body + "</table></br>";
                }
                mailmessage = mailmessage + "BITENLER: ";

                if (listBiten.Count > 0)
                {

                    body = body + "<h3 style='color: white; background-color: #5e97f2'>Süreleri biten şirketler ve şahıslar aşağıda listenlenmiştir;</h3></br><table class='table'><tr style='color: #5e97f2'><th scope='col'>Şirket Adı</th>" +
                        "<th scope='col'>Satış Miktarı</th><th scope='col'>Başlama Tarihi</th><th scope='col'>Bitiş Tarihi</th><th scope='col'>Mobil Hat Sahibi</th><th scope='col'>Mobil Hat Tarihi</th></tr>";
                    foreach (var item in listBiten)
                    {
                        body = body +
                            "<tr><td style='font-weight:bold'>" + item.CompanyName + "</td><td style='text-align: center'>" + item.Sales + "</td><td>" + item.DateofSale + "</td><td>" + item.EndOfContractDate + "</td><td style='text-align: center'>";
                        if (item.IsMobileDataOwn == true)
                        {
                            body = body + "EVET</td><td>" + item.MobileDate + "</td></tr>";
                        }
                        else
                        {
                            body = body + "HAYIR</td><td>" + item.MobileDate + "</td></tr>";
                        }
                        mailmessage = mailmessage + item.CompanyName + ", ";
                    }
                    body = body + "</table>";
                }
                var message = new MailMessage();
                message.To.Add(new MailAddress("myemailadress@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("duyuru@companydomain.xyz", "My Company - Bildirim");  // replace with valid value
                message.Subject = "Süresi Dolacaklar - My Company Bildirim";
                //message.Body = string.Format(body, "burak", "myemailadress@mail.com", "test");
                message.Body = body;
                message.IsBodyHtml = true;

                DateTime dnow = DateTime.Now;
                newMailLog.Message = mailmessage;
                newMailLog.MailDate = dnow;

                db.MailLogs.Add(newMailLog);
                db.SaveChanges();

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
            }


        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {

            // Schedule an IJob to run at an interval
            Schedule<SampleJob>().ToRunNow().AndEvery(1).Days().At(8, 0);

        }
    }
    public class SampleJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;


        public SampleJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;
                    // Do work, son!

                    Job job = new Job();
                    job.Announcement();
                }
            }
            finally
            {
                // Always unregister the job when done.
                HostingEnvironment.UnregisterObject(this);
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
    public class JobHost : IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork(Action work)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                work();
            }
        }
    }
    */
}
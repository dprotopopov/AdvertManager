using System.Web.Mvc;
using AdvertManager.Models;

namespace AdvertManager.Controllers
{
    public class FakeController : Controller
    {
        private readonly FakeDbContext db = new FakeDbContext();
        // GET: Fake
        public ActionResult Index()
        {
            db.Repare();
            return View();
        }
    }
}
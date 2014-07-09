using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdvertManager.Models;
using RT.Domain.Models;

namespace AdvertManager.Controllers
{
    public class RubricCostsController : Controller
    {
        private readonly FakeDbContext db = new FakeDbContext();

        // GET: RubricCosts
        public ActionResult Index()
        {
            return View(db.RubricCostDbSet.ToList());
        }

        // GET: RubricCosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RubricCost> dictionary = db.CostDbSet.ToDictionary(item => item.Id, item => item);
            RubricCost rubricCost = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (rubricCost == null)
            {
                return HttpNotFound();
            }
            return View(rubricCost);
        }

        // GET: RubricCosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RubricCosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RubricId,Cost,Comment,Author,ModifyDate")] RubricCost rubricCost)
        {
            if (ModelState.IsValid)
            {
                db.RubricCostDbSet.Add(rubricCost);
                db.Modified.Add(new Rubric_Cost(rubricCost));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rubricCost);
        }

        // GET: RubricCosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RubricCost> dictionary = db.CostDbSet.ToDictionary(item => item.Id, item => item);
            RubricCost rubricCost = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (rubricCost == null)
            {
                return HttpNotFound();
            }
            return View(rubricCost);
        }

        // POST: RubricCosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RubricId,Cost,Comment,Author,ModifyDate")] RubricCost rubricCost)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(rubricCost).State = EntityState.Modified;
                db.Modified.Add(new Rubric_Cost(rubricCost));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rubricCost);
        }

        // GET: RubricCosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.RubricCost> dictionary = db.CostDbSet.ToDictionary(item => item.Id, item => item);
            RubricCost rubricCost = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (rubricCost == null)
            {
                return HttpNotFound();
            }
            return View(rubricCost);
        }

        // POST: RubricCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dictionary<int, RubricCost> dictionary = db.CostDbSet.ToDictionary(item => item.Id, item => item);
            RubricCost rubricCost = dictionary.ContainsKey(id) ? dictionary[id] : null;
            db.RubricCostDbSet.Remove(rubricCost);
            db.Deleted.Add(new Rubric_Cost(rubricCost));
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
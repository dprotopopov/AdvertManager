using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdvertManager.Models;

namespace AdvertManager.Controllers
{
    public class ActionsController : Controller
    {
        private readonly FakeDbContext db = new FakeDbContext();

        // GET: Actions/Commander
        public ActionResult Commander()
        {
            NameValueCollection pColl = Request.Params;
            string[] strings = pColl.GetValues("action");
            if (strings != null && strings.Any())
            {
                foreach (string action in strings)
                    if (string.Compare(action, "copy", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        string[] values1 = pColl.GetValues("right[]");
                        if (values == null) continue;
                        if (values1 == null) continue;
                        Dictionary<int, RT.Domain.Models.Action> dictionary =
                            db.ActionDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(i)) continue;
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Action left = dictionary[i];
                                RT.Domain.Models.Action right = dictionary[j];
                                if (left == null) continue;
                                if (right == null) continue;
                                RT.Domain.Models.Action action1 = new Action(left).ToObject();
                                action1.Id = 0;
                                action1.AntiActionId = right.Id;
                                action1.ModifyDate = DateTime.Now;
                                db.ActionDbSet.Add(action1);
                                db.Modified.Add(new Action(action1));
                            }
                    }
                    else if (string.Compare(action, "move", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        string[] values1 = pColl.GetValues("right[]");
                        if (values == null) continue;
                        if (values1 == null) continue;
                        Dictionary<int, RT.Domain.Models.Action> dictionary =
                            db.ActionDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(i)) continue;
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Action left = dictionary[i];
                                RT.Domain.Models.Action right = dictionary[j];
                                if (left == null) continue;
                                if (right == null) continue;
                                left.AntiActionId = right.Id;
                                left.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Action(left));
                            }
                    }
                    else if (string.Compare(action, "root", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        if (values == null) continue;
                        Dictionary<int, RT.Domain.Models.Action> dictionary =
                            db.ActionDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                        {
                            if (!dictionary.ContainsKey(i)) continue;
                            RT.Domain.Models.Action left = dictionary[i];
                            if (left == null) continue;
                            left.AntiActionId = 0;
                            left.ModifyDate = DateTime.Now;
                            db.Modified.Add(new Action(left));
                        }
                    }
                    else if (string.Compare(action, "add", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values1 = pColl.GetValues("right[]");
                        if (values1 == null)
                            TempData["actionTemplate"] = new RT.Domain.Models.Action
                            {
                                Id = 0,
                                AntiActionId = 0,
                                ModifyDate = DateTime.Now,
                                ActionDbSet = db.ActionDbSet,
                            };
                        else
                        {
                            Dictionary<int, RT.Domain.Models.Action> dictionary =
                                db.ActionDbSet.ToDictionary(item => item.Id, item => item);
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Action right = dictionary[j];
                                if (right == null) continue;
                                TempData["actionTemplate"] = new RT.Domain.Models.Action
                                {
                                    Id = 0,
                                    AntiActionId = right.Id,
                                    ModifyDate = DateTime.Now,
                                    ActionDbSet = db.ActionDbSet,
                                };
                                break;
                            }
                        }
                        return RedirectToAction("Create");
                    }
                    else if (string.Compare(action, "edit", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values1 = pColl.GetValues("right[]");
                        if (values1 == null) continue;
                        foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            return RedirectToAction("Edit", j);
                    }
                    else if (string.Compare(action, "delete", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values1 = pColl.GetValues("right[]");
                        if (values1 == null) continue;
                        foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            return RedirectToAction("Delete", new { id = j });
                    }
                db.SaveChanges();
            }
            {
                Dictionary<int, RT.Domain.Models.Action> dictionary =
                    db.ActionRootSet.ToDictionary(item => item.Id, item => item);
                int i, j;
                string[] strings1 = pColl.GetValues("leftRoot");
                ViewData["LeftRoot"] = strings1 != null && strings1.Any() &&
                                   dictionary.ContainsKey(i = Convert.ToInt32(strings1.FirstOrDefault()))
                    ? dictionary[i]
                    : new RT.Domain.Models.Action();
                string[] strings2 = pColl.GetValues("rightRoot");
                ViewData["RightRoot"] = strings2 != null && strings2.Any() &&
                                    dictionary.ContainsKey(j = Convert.ToInt32(strings2.FirstOrDefault()))
                    ? dictionary[j]
                    : new RT.Domain.Models.Action();
            }
            ViewData["RootSet"] = db.ActionRootSet;
            return View(db);
        }

        // GET: Actions/Root
        public ActionResult Root()
        {
            return View(db.ActionRootSet.ToList());
        }

        // GET: Actions/Tree
        public ActionResult Tree()
        {
            return View(db.ActionRootSet.ToList());
        }

        // GET: Actions
        public ActionResult Index()
        {
            return View(db.ActionDbSet);
        }

        // GET: Actions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Action> dictionary = db.ActionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Action action = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: Actions/Create
        public ActionResult Create()
        {
            if (TempData["actionTemplate"] != null)
                return View(TempData["actionTemplate"] as RT.Domain.Models.Action);
             return View();
        }

        // Get: Actions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AntiActionId,Title,ModifyDate")] RT.Domain.Models.Action action1)
        {
            if (ModelState.IsValid)
            {
                db.ActionDbSet.Add(action1);
                db.Modified.Add(new Action(action1));
                db.SaveChanges();
                return RedirectToAction("Commander");
            }

            return View(action1);
        }

        // GET: Actions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Action> dictionary = db.ActionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Action action = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // Get: Actions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AntiActionId,Title,ModifyDate")] RT.Domain.Models.Action action1)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(action).State = EntityState.Modified;
                action1.ModifyDate = DateTime.Now;
                db.Modified.Add(new Action(action1));
                if ((action1.AntiActionId ?? 0) != 0)
                {
                    Dictionary<int, RT.Domain.Models.Action> dictionary =
                        db.ActionDbSet.ToDictionary(item => item.Id, item => item);
                    RT.Domain.Models.Action pop = dictionary[(action1.AntiActionId ?? 0)];
                    //db.Entry(pop).State = EntityState.Modified;
                    pop.AntiActionId = action1.Id;
                    db.Modified.Add(new Action(pop));
                }
                db.SaveChanges();
                return RedirectToAction("Commander");
            }
            return View(action1);
        }

        // GET: Actions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Action> dictionary = db.ActionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Action action = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dictionary<int, RT.Domain.Models.Action> dictionary = db.ActionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Action action = dictionary[id];
            db.ActionDbSet.Remove(action);
            db.Deleted.Add(new Action(action));
            db.SaveChanges();
            return RedirectToAction("Commander");
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
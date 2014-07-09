using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdvertManager.Models;
using MyLibrary.Collections;

namespace AdvertManager.Controllers
{
    public class RegionsController : Controller
    {
        private readonly FakeDbContext db = new FakeDbContext();

        // GET: Regions/Commander
        public ActionResult Commander()
        {
            NameValueCollection pColl = Request.Params;
            string[] strings = pColl.GetValues("action");
            if (strings != null && strings.Any())
            {
                foreach (string action in strings)
                    if (string.Compare(action, "move", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        string[] values1 = pColl.GetValues("right[]");
                        if (values == null) continue;
                        if (values1 == null) continue;
                        Dictionary<int, RT.Domain.Models.Region> dictionary =
                            db.RegionDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(i)) continue;
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Region left = dictionary[i];
                                RT.Domain.Models.Region right = dictionary[j];
                                if (left == null) continue;
                                if (right == null) continue;
                                if (right.IdPath.Split('.').Contains(left.Id.ToString())) continue;
                                if (dictionary.ContainsKey(left.ParentId ?? 0))
                                {
                                    RT.Domain.Models.Region parent = dictionary[left.ParentId ?? 0];
                                    parent.HasChild = left.Children.Count > 1;
                                    parent.ModifyDate = DateTime.Now;
                                    db.Modified.Add(new Region(parent));
                                }
                                right.HasChild = true;
                                right.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Region(right));
                                left.ParentId = right.Id;
                                var queue = new StackListQueue<RT.Domain.Models.Region>(left);
                                while (queue.Any())
                                {
                                    RT.Domain.Models.Region dequeue = queue.Dequeue();
                                    //db.Entry(pop).State = EntityState.Modified;
                                    queue.AddRange(dequeue.Children);
                                    var list = new StackListQueue<string>(dequeue.Id.ToString());
                                    short level = 1;
                                    for (RT.Domain.Models.Region current = dequeue;
                                        (current.ParentId??0) != 0;
                                        current = dictionary[current.ParentId??0])
                                    {
                                        list.Add(current.ParentId.ToString());
                                        level++;
                                    }
                                    dequeue.IdPath = string.Join(".", list.GetReverse());
                                    dequeue.Level = level;
                                    dequeue.ModifyDate = DateTime.Now;
                                    db.Modified.Add(new Region(dequeue));
                                }
                            }
                    }
                    else if (string.Compare(action, "root", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        if (values == null) continue;
                        Dictionary<int, RT.Domain.Models.Region> dictionary =
                            db.RegionDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                        {
                            if (!dictionary.ContainsKey(i)) continue;
                            RT.Domain.Models.Region left = dictionary[i];
                            if (left == null) continue;
                            if (dictionary.ContainsKey(left.ParentId ?? 0))
                            {
                                RT.Domain.Models.Region parent = dictionary[left.ParentId ?? 0];
                                parent.HasChild = left.Children.Count > 1;
                                parent.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Region(parent));
                            }
                            left.ParentId = 0;
                            var queue = new StackListQueue<RT.Domain.Models.Region>(left);
                            while (queue.Any())
                            {
                                RT.Domain.Models.Region dequeue = queue.Dequeue();
                                //db.Entry(pop).State = EntityState.Modified;
                                queue.AddRange(dequeue.Children);
                                var list = new StackListQueue<string>(dequeue.Id.ToString());
                                short level = 1;
                                for (RT.Domain.Models.Region current = dequeue;
                                    (current.ParentId??0) != 0;
                                    current = dictionary[current.ParentId??0])
                                {
                                    list.Add(current.ParentId.ToString());
                                    level++;
                                }
                                dequeue.IdPath = string.Join(".", list.GetReverse());
                                dequeue.Level = level;
                                dequeue.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Region(dequeue));
                            }
                        }
                    }
                    else if (string.Compare(action, "add", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values1 = pColl.GetValues("right[]");
                        if (values1 == null)
                            TempData["regionTemplate"] = new RT.Domain.Models.Region
                            {
                                Id = 0,
                                ParentId = 0,
                                Level = 1,
                                ModifyDate = DateTime.Now,
                                RegionDbSet = db.RegionDbSet
                            };
                        else
                        {
                            Dictionary<int, RT.Domain.Models.Region> dictionary =
                                db.RegionDbSet.ToDictionary(item => item.Id, item => item);
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Region right = dictionary[j];
                                TempData["regionTemplate"] = new RT.Domain.Models.Region
                                {
                                    Id = 0,
                                    ParentId = right.Id,
                                    Level = (short) (right.Level + 1),
                                    ModifyDate = DateTime.Now,
                                    RegionDbSet = db.RegionDbSet
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
                Dictionary<int, RT.Domain.Models.Region> dictionary =
                    db.RegionRootSet.ToDictionary(item => item.Id, item => item);
                int i, j;
                string[] strings1 = pColl.GetValues("leftRoot");
                ViewData["LeftRoot"] = strings1 != null && strings1.Any() &&
                                       dictionary.ContainsKey(i = Convert.ToInt32(strings1.FirstOrDefault()))
                    ? dictionary[i]
                    : new RT.Domain.Models.Region();
                string[] strings2 = pColl.GetValues("rightRoot");
                ViewData["RightRoot"] = strings2 != null && strings2.Any() &&
                                    dictionary.ContainsKey(j = Convert.ToInt32(strings2.FirstOrDefault()))
                    ? dictionary[j]
                    : new RT.Domain.Models.Region();
            }
            ViewData["RootSet"] = db.RegionRootSet;
            return View(db);
        }

        // GET: Regions/Root
        public ActionResult Root()
        {
            return View(db.RegionRootSet.ToList());
        }

        // GET: Regions/Tree
        public ActionResult Tree()
        {
            return View(db.RegionRootSet.ToList());
        }

        // GET: Regions
        public ActionResult Index()
        {
            return View(db.RegionDbSet.ToList());
        }

        // GET: Regions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Region> dictionary = db.RegionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Region region = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            if (TempData["regionTemplate"] != null)
                return View(TempData["regionTemplate"] as RT.Domain.Models.Region);
            return View();
        }

        // POST: Regions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,ParentId,Title,ModifyDate,Level,HasChild,IdPath")] RT.Domain.Models.Region region)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, RT.Domain.Models.Region> dictionary = db.RegionDbSet.ToDictionary(item => item.Id,
                    item => item);
                if (dictionary.ContainsKey(region.ParentId ?? 0))
                {
                    RT.Domain.Models.Region parent = dictionary[region.ParentId ?? 0];
                    parent.HasChild = parent.Children.Any();
                    db.Modified.Add(new Region(parent));
                }
                var list = new StackListQueue<string>(region.Id.ToString());
                short level = 1;
                for (RT.Domain.Models.Region current = region;
                    (current.ParentId??0) != 0;
                    current = dictionary[current.ParentId??0])
                {
                    list.Add(current.ParentId.ToString());
                    level++;
                }
                region.IdPath = string.Join(".", list.GetReverse());
                region.Level = level;
                region.ModifyDate = DateTime.Now;
                db.RegionDbSet.Add(region);
                db.Modified.Add(new Region(region));
                db.SaveChanges();
                return RedirectToAction("Commander");
            }

            return View(region);
        }

        // GET: Regions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Region> dictionary = db.RegionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Region region = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,ParentId,Title,ModifyDate,Level,HasChild,IdPath")] RT.Domain.Models.Region region)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, RT.Domain.Models.Region> dictionary =
                    db.RegionDbSet.ToDictionary(item => item.Id, item => item);
                if (dictionary.ContainsKey(region.ParentId ?? 0))
                {
                    RT.Domain.Models.Region parent = dictionary[region.ParentId ?? 0];
                    parent.HasChild = true;
                    db.Modified.Add(new Region(parent));
                }
                var queue = new StackListQueue<RT.Domain.Models.Region>(region);
                while (queue.Any())
                {
                    RT.Domain.Models.Region dequeue = queue.Dequeue();
                    //db.Entry(pop).State = EntityState.Modified;
                    queue.AddRange(dequeue.Children);
                    var list = new StackListQueue<string>(dequeue.Id.ToString());
                    short level = 1;
                    for (RT.Domain.Models.Region current = dequeue;
                        (current.ParentId??0) != 0;
                        current = dictionary[current.ParentId??0])
                    {
                        list.Add(current.ParentId.ToString());
                        level++;
                    }
                    dequeue.IdPath = string.Join(".", list.GetReverse());
                    dequeue.Level = level;
                    dequeue.ModifyDate = DateTime.Now;
                    db.Modified.Add(new Region(dequeue));
                }
                db.SaveChanges();
                return RedirectToAction("Commander");
            }
            return View(region);
        }

        // GET: Regions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Region> dictionary = db.RegionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Region region = dictionary.ContainsKey(id??0) ? dictionary[id??0] : null;
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dictionary<int, RT.Domain.Models.Region> dictionary = db.RegionDbSet.ToDictionary(item => item.Id, item => item);
            RT.Domain.Models.Region region = dictionary[id];
            if (dictionary.ContainsKey(region.ParentId ?? 0))
            {
                RT.Domain.Models.Region parent = dictionary[region.ParentId ?? 0];
                parent.HasChild = parent.Children.Count>1;
                db.Modified.Add(new Region(parent));
            }
            var queue = new StackListQueue<RT.Domain.Models.Region>(region);
            while (queue.Any())
            {
                RT.Domain.Models.Region pop = queue.Dequeue();
                db.RegionDbSet.Remove(pop);
                queue.AddRange(pop.Children);
                db.Deleted.Add(new Region(pop));
            }
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
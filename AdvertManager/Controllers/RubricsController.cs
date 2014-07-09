using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdvertManager.Models;
using MyLibrary.Collections;
using RT.Domain.Models;

namespace AdvertManager.Controllers
{
    public class RubricsController : Controller
    {
        private readonly FakeDbContext db = new FakeDbContext();

        // GET: Rubrics/Commander
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
                        Dictionary<int, RT.Domain.Models.Rubric> dictionary =
                            db.RubricDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(i)) continue;
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Rubric left = dictionary[i];
                                RT.Domain.Models.Rubric right = dictionary[j];
                                if (left == null) continue;
                                if (right == null) continue;
                                if (right.IdPath.Split('.').Contains(left.Id.ToString())) continue;
                                if (dictionary.ContainsKey(left.ParentId ?? 0))
                                {
                                    RT.Domain.Models.Rubric parent = dictionary[left.ParentId ?? 0];
                                    parent.HasChild = left.Children.Count > 1;
                                    parent.ModifyDate = DateTime.Now;
                                    db.Modified.Add(new Rubric(parent));
                                }
                                right.HasChild = true;
                                right.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Rubric(right));
                                left.ParentId = right.Id;
                                var queue = new StackListQueue<RT.Domain.Models.Rubric>(left);
                                while (queue.Any())
                                {
                                    RT.Domain.Models.Rubric dequeue = queue.Dequeue();
                                    //db.Entry(pop).State = EntityState.Modified;
                                    queue.AddRange(dequeue.Children);
                                    var list = new StackListQueue<string>(dequeue.Id.ToString());
                                    short level = 1;
                                    for (RT.Domain.Models.Rubric current = dequeue;
                                        (current.ParentId ?? 0) != 0;
                                        current = dictionary[current.ParentId ?? 0])
                                    {
                                        list.Add(current.ParentId.ToString());
                                        level++;
                                    }
                                    dequeue.IdPath = string.Join(".", list.GetReverse());
                                    dequeue.Level = level;
                                    dequeue.ModifyDate = DateTime.Now;
                                    db.Modified.Add(new Rubric(dequeue));
                                }
                            }
                    }
                    else if (string.Compare(action, "root", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values = pColl.GetValues("left[]");
                        if (values == null) continue;
                        Dictionary<int, RT.Domain.Models.Rubric> dictionary =
                            db.RubricDbSet.ToDictionary(item => item.Id, item => item);
                        foreach (int i in values.Select(s => Convert.ToInt32(s)))
                        {
                            if (!dictionary.ContainsKey(i)) continue;
                            RT.Domain.Models.Rubric left = dictionary[i];
                            if (left == null) continue;
                            if (dictionary.ContainsKey(left.ParentId ?? 0))
                            {
                                RT.Domain.Models.Rubric parent = dictionary[left.ParentId ?? 0];
                                parent.HasChild = left.Children.Count > 1;
                                db.Modified.Add(new Rubric(parent));
                            }
                            left.ParentId = 0;
                            var queue = new StackListQueue<RT.Domain.Models.Rubric>(left);
                            while (queue.Any())
                            {
                                RT.Domain.Models.Rubric dequeue = queue.Dequeue();
                                //db.Entry(pop).State = EntityState.Modified;
                                queue.AddRange(dequeue.Children);
                                var list = new StackListQueue<string>(dequeue.Id.ToString());
                                short level = 1;
                                for (RT.Domain.Models.Rubric current = dequeue;
                                    (current.ParentId ?? 0) != 0;
                                    current = dictionary[current.ParentId ?? 0])
                                {
                                    list.Add(current.ParentId.ToString());
                                    level++;
                                }
                                dequeue.IdPath = string.Join(".", list.GetReverse());
                                dequeue.Level = level;
                                dequeue.ModifyDate = DateTime.Now;
                                db.Modified.Add(new Rubric(dequeue));
                            }
                        }
                    }
                    else if (string.Compare(action, "add", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        string[] values1 = pColl.GetValues("right[]");
                        if (values1 == null)
                            TempData["rubricTemplate"] = new RT.Domain.Models.Rubric
                            {
                                Id = 0,
                                ParentId = 0,
                                Level = 1,
                                RubricDbSet = db.RubricDbSet,
                                ModifyDate = DateTime.Now,
                            };
                        else
                        {
                            Dictionary<int, RT.Domain.Models.Rubric> dictionary =
                                db.RubricDbSet.ToDictionary(item => item.Id, item => item);
                            foreach (int j in values1.Select(s => Convert.ToInt32(s)))
                            {
                                if (!dictionary.ContainsKey(j)) continue;
                                RT.Domain.Models.Rubric right = dictionary[j];

                                TempData["rubricTemplate"] = new RT.Domain.Models.Rubric
                                {
                                    Id = 0,
                                    ParentId = right.Id,
                                    Level = (short) (right.Level + 1),
                                    RubricDbSet = db.RubricDbSet,
                                    ModifyDate = DateTime.Now,
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
                            return RedirectToAction("Delete", new {id = j});
                    }
                db.SaveChanges();
            }
            {
                Dictionary<int, RT.Domain.Models.Rubric> dictionary =
                    db.RubricRootSet.ToDictionary(item => item.Id, item => item);
                int i, j;
                string[] strings1 = pColl.GetValues("leftRoot");
                ViewData["LeftRoot"] = strings1 != null && strings1.Any() &&
                                       dictionary.ContainsKey(i = Convert.ToInt32(strings1.FirstOrDefault()))
                    ? dictionary[i]
                    : new RT.Domain.Models.Rubric();
                string[] strings2 = pColl.GetValues("rightRoot");
                ViewData["RightRoot"] = strings2 != null && strings2.Any() &&
                                        dictionary.ContainsKey(j = Convert.ToInt32(strings2.FirstOrDefault()))
                    ? dictionary[j]
                    : new RT.Domain.Models.Rubric();
            }
            ViewData["RootSet"] = db.RubricRootSet;
            return View(db);
        }

        // GET: Rubrics/Root
        public ActionResult Root()
        {
            return View(db.RubricRootSet.ToList());
        }

        // GET: Rubrics/Tree
        public ActionResult Tree()
        {
            return View(db.RubricRootSet.ToList());
        }

        // GET: Rubrics
        public ActionResult Index()
        {
            return View(db.RubricDbSet.ToList());
        }

        // GET: Rubrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Rubric> dictionary = db.RubricDbSet.ToDictionary(item => item.Id,
                item => item);
            RT.Domain.Models.Rubric rubric = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (rubric == null)
            {
                return HttpNotFound();
            }
            return View(rubric);
        }

        // GET: Rubrics/Create
        public ActionResult Create()
        {
            if (TempData["rubricTemplate"] != null)
                return View(TempData["rubricTemplate"] as RT.Domain.Models.Rubric);
            return View();
        }

        // POST: Rubrics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,ParentId,Title,ModifyDate,Level,HasChild,IdPath")] RT.Domain.Models.Rubric rubric)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, RT.Domain.Models.Rubric> dictionary = db.RubricDbSet.ToDictionary(item => item.Id,
                    item => item);
                if (dictionary.ContainsKey(rubric.ParentId ?? 0))
                {
                    RT.Domain.Models.Rubric parent = dictionary[rubric.ParentId ?? 0];
                    parent.HasChild = true;
                    db.Modified.Add(new Rubric(parent));
                }
                var list = new StackListQueue<string>(rubric.Id.ToString());
                short level = 1;
                for (RT.Domain.Models.Rubric current = rubric;
                    (current.ParentId ?? 0) != 0;
                    current = dictionary[current.ParentId ?? 0])
                {
                    list.Add(current.ParentId.ToString());
                    level++;
                }
                rubric.IdPath = string.Join(".", list.GetReverse());
                rubric.Level = level;
                db.RubricDbSet.Add(rubric);
                db.Modified.Add(new Rubric(rubric));
                db.SaveChanges();
                return RedirectToAction("Commander");
            }

            return View(rubric);
        }

        // GET: Rubrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Rubric> dictionary = db.RubricDbSet.ToDictionary(item => item.Id,
                item => item);
            RT.Domain.Models.Rubric rubric = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (rubric == null)
            {
                return HttpNotFound();
            }
            return View(rubric);
        }

        // POST: Rubrics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,ParentId,Title,ModifyDate,Level,HasChild,IdPath")] RT.Domain.Models.Rubric rubric)
        {
            rubric.Costs.Clear();
            NameValueCollection pColl = Request.Params;
            int childrenCount = pColl.AllKeys.Contains("childrenCount")
                ? Convert.ToInt32(pColl.GetValues("childrenCount").FirstOrDefault())
                : 0;
            for (int i = 0; i < childrenCount; i++)
                if (pColl.AllKeys.Contains(string.Format("Id[{0}]", i)))
                {
                    int Id = 0;
                    int RubricId = 0;
                    decimal Cost = 0m;
                    string Comment = string.Empty;
                    string Author = string.Empty;
                    DateTime ModifyDate = DateTime.Now;
                    try
                    {
                        Id = pColl.AllKeys.Contains(string.Format("Id[{0}]", i))
                            ? Convert.ToInt32(pColl.GetValues(string.Format("Id[{0}]", i)).FirstOrDefault())
                            : 0;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        RubricId = pColl.AllKeys.Contains(string.Format("RubricId[{0}]", i))
                            ? Convert.ToInt32(pColl.GetValues(string.Format("RubricId[{0}]", i)).FirstOrDefault())
                            : 0;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Cost = pColl.AllKeys.Contains(string.Format("Cost[{0}]", i))
                            ? Convert.ToDecimal(pColl.GetValues(string.Format("Cost[{0}]", i)).FirstOrDefault())
                            : 0m;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Comment = pColl.AllKeys.Contains(string.Format("Comment[{0}]", i))
                            ? Convert.ToString(pColl.GetValues(string.Format("Comment[{0}]", i)).FirstOrDefault())
                            : string.Empty;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Author = pColl.AllKeys.Contains(string.Format("Author[{0}]", i))
                            ? Convert.ToString(pColl.GetValues(string.Format("Author[{0}]", i)).FirstOrDefault())
                            : string.Empty;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        ModifyDate = pColl.AllKeys.Contains(string.Format("ModifyDate[{0}]", i))
                            ? Convert.ToDateTime(
                                pColl.GetValues(string.Format("ModifyDate[{0}]", i)).FirstOrDefault())
                            : DateTime.Now;
                    }
                    catch (Exception)
                    {
                    }

                    rubric.Costs.Add(new RubricCost
                    {
                        Id = Id,
                        RubricId = RubricId,
                        Cost = Cost,
                        Comment = Comment,
                        Author = Author,
                        ModifyDate = ModifyDate
                    });
                }
            if (ModelState.IsValid)
            {
                foreach (RubricCost cost in rubric.Costs) cost.RubricId = rubric.Id;
                foreach (RubricCost cost in rubric.Costs) cost.ModifyDate = DateTime.Now;
                db.Modified.AddRange(rubric.Costs.Select(item => new Rubric_Cost(item)));
                Dictionary<int, RT.Domain.Models.Rubric> dictionary =
                    db.RubricDbSet.ToDictionary(item => item.Id, item => item);
                if (dictionary.ContainsKey(rubric.ParentId ?? 0))
                {
                    RT.Domain.Models.Rubric parent = dictionary[rubric.ParentId ?? 0];
                    parent.HasChild = true;
                    db.Modified.Add(new Rubric(parent));
                }
                var stack = new StackListQueue<RT.Domain.Models.Rubric>(rubric);
                while (stack.Any())
                {
                    RT.Domain.Models.Rubric pop = stack.Pop();
                    //db.Entry(pop).State = EntityState.Modified;
                    stack.AddRange(pop.Children);
                    var list = new StackListQueue<string>(pop.Id.ToString());
                    short level = 1;
                    for (RT.Domain.Models.Rubric current = pop;
                        (current.ParentId ?? 0) != 0;
                        current = dictionary[current.ParentId ?? 0])
                    {
                        list.Add(current.ParentId.ToString());
                        level++;
                    }
                    pop.IdPath = string.Join(".", list.GetReverse());
                    pop.Level = level;
                    pop.ModifyDate = DateTime.Now;
                    db.Modified.Add(new Rubric(pop));
                }
                Dictionary<int, RubricCost> rubricCosts =
                    rubric.Costs.Where(item => item.Id != 0).ToDictionary(item => item.Id, item => item);
                db.Deleted.AddRange(
                    db.CostDbSet.Where(cost => cost.RubricId == rubric.Id && !rubricCosts.Keys.Contains(cost.Id))
                        .Select(item => new Rubric_Cost(item)));
                db.SaveChanges();
                return RedirectToAction("Commander");
            }
            return View(rubric);
        }

        // GET: Rubrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary<int, RT.Domain.Models.Rubric> dictionary = db.RubricDbSet.ToDictionary(item => item.Id,
                item => item);
            RT.Domain.Models.Rubric rubric = dictionary.ContainsKey(id ?? 0) ? dictionary[id ?? 0] : null;
            if (rubric == null)
            {
                return HttpNotFound();
            }
            return View(rubric);
        }

        // POST: Rubrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dictionary<int, RT.Domain.Models.Rubric> dictionary = db.RubricDbSet.ToDictionary(item => item.Id,
                item => item);
            RT.Domain.Models.Rubric rubric = dictionary[id];
            if (dictionary.ContainsKey(rubric.ParentId ?? 0))
            {
                RT.Domain.Models.Rubric parent = dictionary[rubric.ParentId ?? 0];
                parent.HasChild = parent.Children.Count > 1;
                db.Modified.Add(new Rubric(parent));
            }
            var stack = new StackListQueue<RT.Domain.Models.Rubric>(rubric);
            while (stack.Any())
            {
                RT.Domain.Models.Rubric pop = stack.Pop();
                db.RubricDbSet.Remove(pop);
                stack.AddRange(pop.Children);
                db.Deleted.Add(new Rubric(pop));
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
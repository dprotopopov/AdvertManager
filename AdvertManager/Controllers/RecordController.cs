using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertManager.Controllers
{
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index()
        {
            return View();
        }

        // GET: Record/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Record/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Record/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Record/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Record/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Record/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

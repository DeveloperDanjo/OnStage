using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnStage.Entities;
using OnStage.Business.Abstract;

namespace OnStage.Presentation.ApplicationModel.Controllers
{ 
    public class ShowController : Controller
    {
        private IShowHandler showHandler;

        public ShowController(IShowHandler showHandler)
        {
            this.showHandler = showHandler;
        }

        //
        // GET: /Show/

        public ViewResult Index()
        {
            return View(showHandler.GetAllShows());
        }

        //
        // GET: /Show/Details/5

        public ViewResult Details(int id)
        {
            var show = showHandler.GetShow(id);
            return View(show);
        }

        //
        // GET: /Show/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Show/Create

        [HttpPost]
        public ActionResult Create(Show show)
        {
            if (ModelState.IsValid)
            {
                //showHandler.AddShow(show);
                return RedirectToAction("Index");  
            }

            return View(show);
        }
        
        //
        // GET: /Show/Edit/5
 
        public ActionResult Edit(int id)
        {
            var show = showHandler.GetShow(id);
            return View(show);
        }

        //
        // POST: /Show/Edit/5

        [HttpPost]
        public ActionResult Edit(Show show)
        {
            if (ModelState.IsValid)
            {
                //showHandler.SaveShow(show);
                return RedirectToAction("Index");
            }
            return View(show);
        }

        //
        // GET: /Show/Delete/5
 
        public ActionResult Delete(int id)
        {
            var show = showHandler.GetShow(id);
            return View(show);
        }

        //
        // POST: /Show/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            //showHandler.DeleteShow(id);
            return RedirectToAction("Index");
        }
    }
}
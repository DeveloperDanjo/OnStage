using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ent = OnStage.Entities;
using OnStage.Business.Abstract;
using Pres = OnStage.Presentation.ApplicationModel.Models;

namespace OnStage.Presentation.ApplicationModel.Controllers
{ 
    public class CueBookController : Controller
    {
        private ICueBookHandler cueBookHandler;

        public CueBookController(ICueBookHandler cueBookHandler)
        {
            this.cueBookHandler = cueBookHandler;
        }

        public ActionResult Run(int id)
        {
            return View(cueBookHandler.GetCueBook(id));
        }

        public JsonResult Details(int id)
        {
            return Json(new Pres.CueBook(cueBookHandler.GetCueBook(id)), JsonRequestBehavior.AllowGet);
        }
    }
}
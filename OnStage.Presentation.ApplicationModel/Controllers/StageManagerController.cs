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
    public class StageManagerController : Controller
    {
        private IStageManagerCueBookHandler stageManagerCueBookHandler;

        public StageManagerController(IStageManagerCueBookHandler stageManagerCueBookHandler)
        {
            this.stageManagerCueBookHandler = stageManagerCueBookHandler;
        }

        public ActionResult Run(int id)
        {
            return View(stageManagerCueBookHandler.GetStageManagerCueBook(id));
        }

        public JsonResult Details(int id)
        {
            return Json(new Pres.StageManagerCueBook(stageManagerCueBookHandler.GetStageManagerCueBook(id)), JsonRequestBehavior.AllowGet);
        }
    }
}
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

        public ActionResult Index()
        {
            return View(showHandler.GetAllShows());
        }

        public ActionResult Details(int id)
        {
            return View(showHandler.GetShow(id));
        }

        public ActionResult Script(int id)
        {
            var script = showHandler.GetShow(id).Script;
            return File(script.Data, script.MimeType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using OnStage.Business.Abstract;

namespace OnStage.Presentation.ApplicationModel.Controllers
{
    public class HomeController : Controller
    {

        public IShowHandler showHandler;

        public HomeController(IShowHandler showHandler)
        {
            this.showHandler = showHandler;
        }

        public ActionResult Index()
        {
            var model = showHandler.GetAllShows();

            return View(model);
        }

    }
}

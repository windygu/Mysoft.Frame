using Mysoft.Core;
using Mysoft.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Web.Controllers
{
    public class MetaClassController : Controller
    {
        //
        // GET: /MetaClass/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllMetaClass()
        {
            return Json(AjaxResult.New(MetaBusiness.GetAllClass()),JsonRequestBehavior.AllowGet);
        }
        public ActionResult MetaClassListView()
        {
            return View();
        }

        public ActionResult MetaClassDetailView()
        {
            return View();
        }
    }
}

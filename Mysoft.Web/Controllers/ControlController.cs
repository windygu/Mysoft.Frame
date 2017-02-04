using Mysoft.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Web.Controllers
{
    public class ControlController : ControllerBase
    {
       
        public ActionResult MetaClassLookUp()
        {
            ViewBag.MetaClassDefines = MetaBusiness.GetAllClass();
            return View();
        }

    }
}

using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Web
{
    public class ControllerBase : Controller
    { 

        protected new JsonResult Json(object data)
        {
            return new NetJsonResult(data);
        }
        protected new JsonResult Json(object data,JsonRequestBehavior behavior)
        {
            if (behavior == JsonRequestBehavior.DenyGet) {
                return base.Json(data, behavior);
            }
            return new NetJsonResult(data);
        }

    }


}

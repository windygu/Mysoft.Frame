using Mysoft.Core;
using Mysoft.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Web.Controllers
{
    public class MetaClassController : ControllerBase
    {
        //
        // GET: /MetaClass/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllMetaClass()
        {
            return Json(AjaxResult.New(MetaBusiness.GetAllClass()));
        }
        public ActionResult MetaClassListView()
        {
            return View();
        }

        #region detail

        public JsonResult GetMetaClass(string id)
        {
            return Json(AjaxResult.New(MetaBusiness.GetMetaClassDefine(id)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MetaClassDetailView()
        {
            return View();
        }

      
        #endregion


        #region 属性集
        public JsonResult GetMetaProperties(string classId)
        {
            return Json(AjaxResult.New(MetaBusiness.GetPropertyDefines(classId)));
        }
        public JsonResult SaveMetaProperty(MetaPropertyDefine property)
        {
            return Json(AjaxResult.New(MetaBusiness.SavePropertyDefine(property)));
        }
        public JsonResult DeleteMetaProperty(string id)
        {
            return Json(AjaxResult.New(MetaBusiness.DeletePropertyDefine(id)));
        }

        #endregion
    }
}

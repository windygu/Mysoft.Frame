using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mysoft.Infrastructure
{
    public class ControllerBase:Controller
    {
        #region 重构页面方法
        protected virtual ViewResult ObjView(string objClsId)
        {
            return new ObjViewResult(objClsId);
        }

       
        #endregion

        #region 重构json序列化，使用json.net
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new NetJsonResult() { Data = data,ContentEncoding = contentEncoding,ContentType = contentType };
        }

        protected new JsonResult Json(object data)
        {
            return new NetJsonResult() { Data = data };
        }
        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
        {
            return new NetJsonResult() { Data = data };
        }


        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NetJsonResult() { Data = data, ContentEncoding = contentEncoding, ContentType = contentType };
        }

        #endregion
    }

    public class ObjViewResult : ViewResult
    {
        public string ObjClsId { get; set; }
        public ObjViewResult(string clsId)
        {
            this.ObjClsId = clsId;
        }

    }


    public class NetJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            response.ContentType = ContentType.IfNullOrEmptyThen("application/json");
            response.ContentEncoding = ContentEncoding ?? Encoding.UTF8;
            if (Data != null) {
                response.Write(Data.SerializeToJson());
            }

        } 
    }
}

using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mysoft.Web
{
    public class NetJsonResult : JsonResult
    {
        public NetJsonResult() : base()
        { }
        public NetJsonResult(object data) : this()
        {
            this.Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            response.ContentType = ContentType.IfNullOrEmptyThen("application/json");
            response.ContentEncoding = ContentEncoding ?? Encoding.UTF8;
            if (Data != null)
            {
                response.Write(JsonHelper.SerializeObject(Data));
            }

        }
    }
}
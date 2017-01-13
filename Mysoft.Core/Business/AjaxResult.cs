using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{ 
  
    public class AjaxResult<T>
    {
        public bool Result { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
    public class AjaxResult : AjaxResult<object>
    {
        public AjaxResult() { }
        public AjaxResult(object data) {
            Result = true;
            Data = data;
        }
        public AjaxResult(string msg) {
            Result = false;
            Message = msg;
        }

        public static AjaxResult New(string msg) {
            return new AjaxResult(msg);
        }
        public static AjaxResult New(object data)
        {
            return new AjaxResult(data);
        }
    }

    public class PageResult<T>:AjaxResult<List<T>>
    {
        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

    }

    public class PageResult : PageResult<object>
    {
      
    }
}

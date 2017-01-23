using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Mysoft.Infrastructure
{
    public static  class Extensions
    {
        public static string SerializeToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        

        public static string IfNullOrEmptyThen(this string str, string value)
        {
            if (string.IsNullOrEmpty(str))
            {
                return value;
            }
            return str;
        }
    }


}

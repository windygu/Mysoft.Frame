using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class DbColFuncAttribute:Attribute
    {
        public DbColFuncAttribute(Func<object, object> func)
        {
            this.ColFunc = func;
        }
        public Func<object,object> ColFunc { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class MyException : Exception
    {
        public string ErrorCode { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class MyException : Exception
    {
        public MyException() { }
        public MyException(string msg) : base(msg) { }
        public virtual string ErrorCode { get; set; } 
        public virtual string Type { get;}

    }

    public class BusinessException : MyException
    {
        public override string Type
        {
            get
            {
                return "业务信息";
            }
        }
        public BusinessException() {

        }
        public BusinessException(string msg):base(msg)
        {
            
        }
    }
}

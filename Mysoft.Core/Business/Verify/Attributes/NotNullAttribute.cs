using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{  
    /// <summary>
    /// 不为空校验特性
    /// </summary>
    public class NotNullAttribute : VerifyAttribute
    {
        public NotNullAttribute() { }
        public NotNullAttribute(string name)
        {
            this.PropertyName = name;
        }
        public override VerifyResult Verify(object obj)
        {            
            if (obj == null || obj.ToStr() == string.Empty)
                return VerifyResult.CanNotNull(this.PropertyName);
            else
                return VerifyResult.OK;
        }
    }
}

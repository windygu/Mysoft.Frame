using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    /// <summary>
    /// 校验接口
    /// </summary>
    public interface IVerify
    {
        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        VerifyResult Verify();
    }

   
}

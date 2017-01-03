using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    /// <summary>
    /// 快速校验实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class FastVerifyEntity<TKey> : EntityBase<TKey>,IVerify
    {
        public virtual VerifyResult Verify()
        {
            var result = this.GetType().GetProperties().Select(x=>  
                x.GetCustomAttributes(true)
                    .OfType<VerifyAttribute>()
                    .Select(y=>y.Verify(x.FastGetValue(this)))
                    .FirstOrDefault(y=>!y.Result) ?? VerifyResult.OK
            ).FirstOrDefault(y=>y!=null && !y.Result);
            return result ?? VerifyResult.OK;
        } 
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{ 
    /// <summary>
    /// 校验特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class VerifyAttribute:Attribute
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract VerifyResult Verify(object entity);
    } 
}

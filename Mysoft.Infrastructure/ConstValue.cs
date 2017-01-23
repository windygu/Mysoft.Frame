using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Infrastructure
{
    /// <summary>
    /// 常量
    /// </summary>
    public class ConstValue
    {
        #region 指定类型
        public static readonly Type TypeOfString = typeof(string);

        public static readonly Type TypeOfInt = typeof(int);

        public static readonly Type TypeOfDateTime = typeof(DateTime);
        #endregion
        #region session Key
        public static readonly string SessionKey_Identity = "common_Identity";
        #endregion
    }
}

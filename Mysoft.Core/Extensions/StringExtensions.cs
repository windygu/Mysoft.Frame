using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public static class StringExtensions
    {
        #region 字符串
        /// <summary>
        /// 判断字符串是否相等
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="com"></param>
        /// <returns></returns>
        public static bool IsSame(this string str1, string str2, StringComparison com = StringComparison.OrdinalIgnoreCase)
        {
            return str1.Equals(str2, com);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        #endregion


    }
}

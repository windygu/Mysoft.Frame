using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mysoft.Core
{
    public static class RefectionExtensions
    {

        #region 反射相关
        /// <summary>
        /// 类型是否为简单类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsValueType || type == ConstValue.TypeOfString;
        }

        #endregion


        #region 快速反射扩展方法
        /// <summary>
        /// 快速获取属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object FastGetValue(this PropertyInfo property, object instance)
        {
            var getter = DynamicCalls.GetPropertyGetter(property);
            return getter(instance);
        }

        /// <summary>
        /// 快速给属性赋值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public static void FastSetValue(this PropertyInfo property, object instance, object value)
        {
            var setter = DynamicCalls.GetPropertySetter(property);
            setter(instance, value);
        }

        #endregion
    }
}

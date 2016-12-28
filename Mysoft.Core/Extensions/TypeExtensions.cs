using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public static class TypeExtensions
    {

        #region 类型转换
        public static int ToInt(this object @this)
        {
            return Convert.ToInt32(@this);
        }
        public static int ToIntOrDefault(this object @this, int defaultValue = 0)
        {
            return (int)@this.ToDoubleOrDefault(defaultValue);
        }


        public static double ToDouble(this object @this)
        {
            return Convert.ToDouble(@this);
        }
        public static double ToDoubleOrDefault(this object @this, double defaultValue = 0)
        {
            double value;
            return double.TryParse(@this + "", out value) ? value : defaultValue;
        }

        public static float ToFloat(this object @this)
        {
            return Convert.ToSingle(@this);
        }
        public static float ToFloatOrDefault(this object @this, float defaultValue = 0)
        {
            return (float)@this.ToDoubleOrDefault(defaultValue);
        }

        public static decimal ToDecimal(this object @this)
        {
            return Convert.ToDecimal(@this);
        }
        public static decimal ToDecimalOrDefault(this object @this, decimal defaultValue = 0)
        {
            decimal value;
            return decimal.TryParse(@this + "", out value) ? value : defaultValue;
        }

        public static bool ToBool(this object @this)
        {
            return Convert.ToBoolean(@this);
        }
        public static bool ToBoolOrDefault(this object @this, bool defaultValue)
        {
            bool value;
            return bool.TryParse(@this + "", out value) ? value : defaultValue;
        }

        public static DateTime ToDateTime(this object @this)
        {
            return Convert.ToDateTime(@this);
        }

        public static DateTime ToDateTimeOrDefault(this object @this)
        {
            return @this.ToDateTimeOrDefault(DateTime.Now);
        }
        public static DateTime ToDateTimeOrDefault(this object @this, DateTime defaultValue)
        {
            DateTime dt;
            return DateTime.TryParse(@this + "", out dt) ? dt : defaultValue;
        }

        public static object To(this object @this, Type type)
        {
            if (@this == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (@this == null) return null;
            if (type == @this.GetType()) return @this;
            if (type.IsEnum)
            {
                if (@this is string)
                    return Enum.Parse(type, @this as string);
                else
                    return Enum.ToObject(type, @this);
            }
            //if (!type.IsInterface && type.IsGenericType)
            //{
            //    Type innerType = type.GetGenericArguments()[0];
            //    object innerValue = ChangeType(value, innerType);
            //    return Activator.CreateInstance(type, new object[] { innerValue });
            //}
            if (@this is string && type == typeof(Guid)) return new Guid(@this as string);
            if (@this is string && type == typeof(Version)) return new Version(@this as string);
            if (!(@this is IConvertible)) return @this;
            return Convert.ChangeType(@this, type);
        }

        public static T To<T>(this object @this)
        {
            return (T)@this.To(typeof(T));
        }

        public static T ToOrDefault<T>(this object @this)
        {
            return @this.ToOrDefault<T>(default(T));
        }
        public static T ToOrDefault<T>(this object @this, T defaultValue)
        {
            try
            {
                return (T)@this.To(typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion
        /// <summary>
        /// 是否是简单值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(this Type type)
        {
            return type.IsValueType || type == ConstValue.TypeOfString;
        }
    }
}

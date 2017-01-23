using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Infrastructure
{
    public  static class CollectionExtensions
    {

        #region IEnumerable
        public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }


        #endregion

        #region dictionary
        /// <summary>
        /// 获取或者新增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="valueFunc"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, Func<TKey,TValue> valueFunc)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                dic[key] = valueFunc(key);
                return dic[key];
            }
        }
        /// <summary>
        /// 获取或者新增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="valueFunc"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, Func<TValue> valueFunc)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                dic[key] = valueFunc();
                return dic[key];
            }
        }
        /// <summary>
        /// 获取或者新增
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else {
                dic[key] = value;
                return value;
            }
        }
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion

    }
     
}

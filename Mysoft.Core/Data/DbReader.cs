using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
namespace Mysoft.Core
{
    public class DbReader
    {
        private DbDataReader _dr;
        internal DbReader(DbDataReader dr)
        {
            this._dr = dr;
        }

        public object this[string key]
        {
            get { return _dr[key]; }
        }
        public object this[int ordinal]
        {
            get { return _dr[ordinal]; }
        }

        public  T GetValueOrDefault<T>(string key,T defaultValue = default(T))
        {
            int ordinal = _dr.GetOrdinal(key);
            if (ordinal < 0)
            {
                return defaultValue;
            }
            else {
                return _dr[ordinal].To<T>();
            }
        }
        public T? GetValue<T>(string key)where T:struct
        {
            int ordinal = _dr.GetOrdinal(key);
            if (ordinal < 0)
            {
                return null;
            }
            else
            {
                return  (T?)_dr[ordinal].To<T>();
            }
        }
        public string GetValue(string key) 
        {
            int ordinal = _dr.GetOrdinal(key);
            if (ordinal < 0)
            {
                return null;
            }
            else
            {
                return _dr[ordinal]?.ToString();
            }
        }
         
    }
}

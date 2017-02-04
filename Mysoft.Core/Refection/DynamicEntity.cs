using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Mysoft.Core
{
    public class DynamicEntity<T> : DynamicObject
    {
        public DynamicEntity() { }

        public DynamicEntity(Dictionary<string, T> dic)
        {
            if (dic != null)
                propertyDic = dic;
        }

        Dictionary<string, T> propertyDic = new Dictionary<string, T>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            T val;
            var ret = propertyDic.TryGetValue(name, out val);
            result = val;
            return ret;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var name = binder.Name;
            if (propertyDic.ContainsKey(name))
            {
                propertyDic[name] = (T)value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public T GetProperty(string name)
        {
            return propertyDic[name];
        }
        public void SetProperty(string name, T value, bool isThrowWhileNotExist = true)
        {
            if (propertyDic.ContainsKey(name))
            {
                propertyDic[name] = value;

            }
            else
            {
                if (isThrowWhileNotExist)
                    throw new Exception("属性不存在");
            }

        }
        public void AddProperty(string name, T value = default(T))
        {
            propertyDic[name] = value;
        }

        public void InitProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                AddProperty(key, default(T));
            }
        }
        public void InitProperties(IDictionary<string, T> properties)
        {
            foreach (var p in properties)
            {
                AddProperty(p.Key, p.Value);
            }
        }

        public void SetProperties(IDictionary<string, T> properties)
        {
            foreach (var p in properties)
            {
                SetProperty(p.Key, p.Value, false);
            }
        }
    }

    /// <summary>
    /// 动态实体
    /// </summary>
    public class DynamicEntity : DynamicObject
    {
        public DynamicEntity() { }

        public DynamicEntity(Dictionary<string, object> dic)
        {
            if (dic != null)
                dictionary = dic;
        }

        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            return dictionary.TryGetValue(name, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dictionary[binder.Name.ToLower()] = value;
            return true;
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return dictionary.Keys;
        }


        public virtual object this[string name]
        {
            get { return dictionary.GetValueOrDefault(name.ToLower()); }
            set { dictionary[name.ToLower()] = value; }
        }

        public bool Contains(string name)
        {
            return dictionary.ContainsKey(name.ToLower());
        }
    }
}

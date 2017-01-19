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
                dictionary = dic;
        }

        Dictionary<string, T> dictionary = new Dictionary<string, T>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            T val;
            var r = dictionary.TryGetValue(name, out val);
            result = (object)val;
            return r;
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

    /// <summary>
    /// 动态实体
    /// </summary>
    public class DynamicEntity: DynamicObject
    {
        public DynamicEntity() { }

        public DynamicEntity(Dictionary<string, object> dic)
        {
            if(dic!=null)
                dictionary = dic;
        }

        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public override bool TryGetMember( GetMemberBinder binder, out object result)
        { 
            string name = binder.Name.ToLower(); 
            return dictionary.TryGetValue(name, out result);
        }
        public override bool TrySetMember( SetMemberBinder binder, object value)
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

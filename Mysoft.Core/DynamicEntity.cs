using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Mysoft.Core
{
    /// <summary>
    /// 动态实体
    /// </summary>
    public class DynamicEntity: DynamicObject
    {
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

        public virtual void AddOrSetMember(string key, object value)
        {
            dictionary[key.ToLower()] = value;
        }
        public object this[string name]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Infrastructure
{
    public class IocContainer
    {
        private  Dictionary<Type, ServiceTypeInfo> cache = new Dictionary<Type, ServiceTypeInfo>();
        public void RegisterType(Type from, Type to)
        {
            if (cache.ContainsKey(from))
            {
                cache[from].Add(to);
            }
            else {
                cache[from] = new ServiceTypeInfo(to);
            }
        }
          
        public ServiceTypeInfo GetServiceType(Type from)
        {
            return cache[from];
        }
    }

   
}

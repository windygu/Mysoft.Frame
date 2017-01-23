using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Infrastructure
{
    public class ServiceTypeInfo
    {
        public List<Type> Types { get; }

        public Type CurrentType { get; protected set; }
        public object Singleton { get; set; }

        public ServiceTypeInfo()
        {
            Types = new List<Type>();
        }
        public ServiceTypeInfo(Type type) : this()
        {
            Add(type);
        }
        public virtual void Add(Type type)
        {
            Types.Add(type);
            CurrentType = type;
        }

        public virtual void AddOnly(Type type)
        {
            Types.Clear();
            Types.Add(type);
            CurrentType = type;
        }

    }
}

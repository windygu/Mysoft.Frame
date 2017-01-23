using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public abstract class BusinessBase:EntityBase<string>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ObjectState State { get; set; }

        public virtual void Save()
        {

        }

        public virtual void Delete()
        {

        }

        public virtual void Publish()
        {

        }
    }
}

using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public abstract class BusinessEntityBase:EntityBase<string>
    {
       

        public abstract void Insert();
        public abstract void Update();

        public abstract int Delete();
    }
}

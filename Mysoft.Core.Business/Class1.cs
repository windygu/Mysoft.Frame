using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Business
{
    public abstract class BusinessEntityBase : EntityBase<string>
    {
        public string ObjClsId { get; set; }
    }
}

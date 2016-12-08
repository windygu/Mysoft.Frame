using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public abstract class EntityBase
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public DateTime CreateOn { get; set; }
    }
}

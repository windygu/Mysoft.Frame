using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{ 

    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; } 

        public string Remark { get; set; }

        public DateTime CreateOn { get; set; }

    }

    
}

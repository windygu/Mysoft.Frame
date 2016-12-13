using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Core;
namespace Mysoft.DataManager
{
    public class MetaDomainBusiness
    {
        public static List<MetaDomain> GetDomains()
        {
            return DbQuery.GetList<MetaDomain>();
        }

        
    }
}

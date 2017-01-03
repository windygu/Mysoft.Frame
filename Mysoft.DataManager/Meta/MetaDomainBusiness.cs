using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Core;
namespace Mysoft.DataManager
{
    public class MetaBusiness
    {
        #region domain
        public static List<MetaDomain> GetDomains()
        {
            return DbQuery.GetList<MetaDomain>();
        }

        public static int AddDomain(MetaDomain domain)
        {            
            domain.Verify().ThrowWhileError();
            return DbQuery.Insert(domain);
        }

        #endregion  

        public static MetaClassDefine GetMetaClassDefine(string classId)
        {
            const string sc = @"select * from MetaClassDefines where Id=@Id";
            const string sp = @"select * from  MetaPropertyDefine where ClassId=@Id";
            using (var db = DbQuery.New(true)) {
                var param = new { Id = classId };
                var cls = db.ExecuteSingle<MetaClassDefine>(sc, param);
                cls.PropertyDefineList = db.ExecuteList<MetaPropertyDefine>(sp, param);
                return cls;
            }
        }
        


    }
}

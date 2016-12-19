using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Core;
namespace Mysoft.DataManager
{
    public class MetaBusiness
    {
        public static List<MetaDomain> GetDomains()
        {
            return DbQuery.GetList<MetaDomain>();
        }

        public static MetaClassDefine GetMetaClassDefine(string classId)
        {
            const string sql = @"
                    select * from MetaClassDefines c where c.Id=@Id;
                    select * from  MetaPropertyDefine where ClassId=@Id
                    ";
            return DbQuery.New().ExecuteList<MetaClassDefine, MetaPropertyDefine>(sql, new { Id = classId }, (x, y) =>
            {
                x.ForEach(x1 => x1.PropertyDefineList.AddRange(y.Where(y1 => y1.ClassId == x1.Id)));
                return x;
            }).FirstOrDefault();
        }
        


    }
}

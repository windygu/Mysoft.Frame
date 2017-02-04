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
            const string sp = @"select * from  MetaPropertyDefines where ClassId=@Id";
            using (var db = DbQuery.New(true)) {
                var param = new { Id = classId };
                var cls = db.ExecuteSingle<MetaClassDefine>(sc, param);
                cls.PropertyDefineList = db.ExecuteList<MetaPropertyDefine>(sp, param);
                return cls;
            }
        }


        public static List<MetaClassDefine> GetAllClass()
        {
            const string sql1 = "select * from MetaClassDefines";
            const string sql2 = "select * from MetaPropertyDefines";
            using (var db = DbQuery.New(true)) {
                var list = db.ExecuteList<MetaClassDefine>(sql1);
                var ps = db.ExecuteList<MetaPropertyDefine>(sql2);
                list.ForEach(x => x.PropertyDefineList = ps.Where(p => p.ClassId == x.Id).ToList());
                return list;
            }
        }

        public static string GetTableNameByObjClassId(string objClsId)
        {
            const string sql = "select tablename from MetaClassDefines where Id=@Id";
            return DbQuery.New().ExecuteScalar<string>(sql, new { Id = objClsId });
        }

        #region 属性集
        public static MetaPropertyDefine SavePropertyDefine(MetaPropertyDefine p)
        {
            if (p.Id.IsNullOrEmpty())
            {
                p.Id = Guid.NewGuid().ToString();
                DbQuery.Insert(p);
            }
            else {
                DbQuery.Update(p);
            }
            return p;
        }

        public static List<MetaPropertyDefine> GetPropertyDefines(string classId)
        {
            const string sql2 = "select * from MetaPropertyDefines where classid=@ClassId";
            return DbQuery.New().ExecuteList<MetaPropertyDefine>(sql2, new { ClassId = classId });
        }
        public static int DeletePropertyDefine(string id)
        {
            const string sql = "delete from MetaPropertyDefines where id =@Id";
            return DbQuery.New().ExecuteNoQuery(sql, new { Id = id });
        }
        #endregion
    }
}

using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public abstract class DataObjBase<T> : DynamicObjBase where T : DataObjBase<T>,new()
    {
        protected virtual MetaClassDefine MetaClassDefine { get; set; }
      
        public DataObjBase() { }
        public DataObjBase(string clsId)
        {
            SetObjClsId(clsId);
        }
        protected virtual void SetObjClsId(string clsId)
        {
            MetaClassDefine = MetaBusiness.GetMetaClassDefine(clsId);
            InitProperties(MetaClassDefine.PropertyDefineList.Select(x => new KeyValuePair<string, object>(x.Name, x.DefaultValue)));
            ClsId = clsId;
        }
   
        public static T GetObj(string clsId, string id)
        {
            var obj = new T();
            obj.SetObjClsId(clsId);
            if (obj.MetaClassDefine == null)
                throw new Exception("对象类架构未初始化");
            if (string.IsNullOrEmpty(id))
                throw new Exception("id为空，无法初始化对象数据");
            var sqlCreator = new SqlCreator(obj.MetaClassDefine.TableName, obj.MetaClassDefine.PropertyDefineList.Select(x => x.ColName));
            string sql = sqlCreator.CreateSql_SelectById();
            using (var dr = DbQuery.New().ExecuteDataReader(sql, new { Id = id }))
            {
                dr.Read();
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    obj.SetProperty(dr.GetName(i), dr.GetValue(i));
                }
            }
            return obj as T;
        }

        public static T NewObj(string clsId)
        {
            var obj = new T();
            obj.SetObjClsId(clsId);
            obj.Id = Guid.NewGuid().ToString();
            return obj;
        }
    }
}

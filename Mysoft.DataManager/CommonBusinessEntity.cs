using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Mysoft.DataManager
{
    public class DynamicPropertyCollection : DynamicEntity
    {
        public DynamicPropertyCollection(Dictionary<string, object> ps):base(ps)
        {

        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Contains(binder.Name.ToLower()))
            {
                base[binder.Name.ToLower()] = value;
                return true;
            }
            else {
                throw new Exception(string.Format("对象属性{0} 不存在或者无权限访问",binder.Name));
            }
           
          
        }
        public override object this[string name]
        {
            get
            {
                return base[name];
            }

            set
            {
                if (Contains(name.ToLower()))
                {
                    base[name] = value; 
                }
                else
                {
                    throw new Exception(string.Format("对象属性{0} 不存在或者无权限访问", binder.Name));
                }
            }
        }
        
    }
    public class DynamicTabProperty : DynamicPropertyCollection
    {
        public DynamicTabProperty(Dictionary<string, object> ps) : base(ps)
        {
        }
    }


    public class CommonBusinessEntity : BusinessEntityBase
    {
        private CommonBusinessEntity(string objClsId){

        }

        public string ObjClsId { get; set; }
        
        public DynamicPropertyCollection Properties { get; private set; }  

        public DynamicTabProperty TabProperties { get; private set; }

        public dynamic DynamicProperties
        {
            get { return (dynamic)Properties; }
        }

        

        public override int Delete()
        {
            var table = MetaBusiness.GetTableNameByObjClassId(ObjClsId);
            string sql = string.Format("delete from  {0} where id = '{1}'", table, Id);
            return DbQuery.New().ExecuteNoQuery(sql);
        }

        public override void Insert()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }



        #region Static Methods


        #endregion
    }
}

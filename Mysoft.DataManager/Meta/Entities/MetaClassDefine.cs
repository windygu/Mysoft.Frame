using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public class MetaClassDefine : EntityBase<string>
    {
        /// <summary>
        /// 类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<MetaPropertyDefine> PropertyDefineList { get; set; }

      
    }
}

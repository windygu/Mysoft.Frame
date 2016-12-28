using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    /// <summary>
    /// 元数据属性定义
    /// </summary>
    public class MetaPropertyDefine : EntityBase<string>
    {
        #region 基础属性
        public string Name { get; set; }

        public string ClassId { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public MetaDataType DataType { get; set; }

        /// <summary>
        /// 属性长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int Digits { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsCanNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否有默认值
        /// </summary>
        public bool IsHasDefaultValue { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        #endregion  

        #region 引用属性
        /// <summary>
        /// 是否引用属性
        /// </summary>
        public bool IsUsing { get; set; }

        /// <summary>
        /// 引用的类id
        /// </summary>
        public string UsingClassId { get; set; }

        /// <summary>
        /// 引用的属性Id
        /// </summary>
        public string UsingPropertyId { get; set; }
        #endregion

        #region 数据库相关
        /// <summary>
        /// 数据库列名称
        /// </summary>
        public string ColName { get; set; }
        #endregion

        #region 显示方式
        /// <summary>
        /// 显示方式
        /// </summary>
        public MetaShowType ShowType { get; set; }

        
        /// <summary>
        /// 选择项
        /// </summary>
        public Dictionary<string, string> Options { get; set; }

        #endregion



    }
}

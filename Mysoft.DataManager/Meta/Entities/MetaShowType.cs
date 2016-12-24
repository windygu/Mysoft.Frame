using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public enum MetaShowType
    {
        /// <summary>
        /// 文本框
        /// </summary>
        TextBox=1,
        /// <summary>
        /// 多行文本框
        /// </summary>
        TextArea=2,
        /// <summary>
        /// 复选框
        /// </summary>
        CheckBox=3,
        /// <summary>
        /// 下拉框
        /// </summary>
        ComboBox=4,

        /// <summary>
        /// 日期选择
        /// </summary>
        DataPick= 5,

        /// <summary>
        /// 自定义
        /// </summary>
        Self=99,

    }
}

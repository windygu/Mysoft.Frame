using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public enum ObjectState
    {
        /// <summary>
        /// 创建
        /// </summary>
        Creating =1,

        /// <summary>
        /// 活动状态
        /// </summary>
        Active =2,
        /// <summary>
        /// 发布状态
        /// </summary>
        Publish=3,
    }
}

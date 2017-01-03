using Mysoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    public  class MetaDomain : FastVerifyEntity<string>
    {
        [NotNull("名称")]
        public string Name { get; set; }

        public string ParentId { get; set; }

        public string PathIds { get; set; } 

        public string FullName { get; set; }

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsDir { get; set; }
         
    }
}

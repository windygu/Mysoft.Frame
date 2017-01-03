using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    /// <summary>
    /// 校验结果类
    /// </summary>
    public class VerifyResult
    {
        /// <summary>
        /// 校验结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 校验错误信息
        /// </summary>
        public string Message { get; set; }

        public VerifyResult(string msg)
        {
            this.Result = false;
            this.Message = msg;
        }
        public VerifyResult()
        {
            this.Result = true;
        }

        public BusinessException ThrowWhileError()
        {
            return new BusinessException(this.Message);
        }


        /// <summary>
        /// 不能为空
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static VerifyResult CanNotNull(string name)
        {
            return new VerifyResult(string.Format("{0}不能为空", name));
        }

        /// <summary>
        /// 成功
        /// </summary>
        public static VerifyResult OK { get; } = new VerifyResult();

    }

}

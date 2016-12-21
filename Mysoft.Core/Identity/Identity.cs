using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mysoft.Core
{
    public class Identity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        internal string Password { get; set; }

        public static Identity Current
        {
            get {
                return HttpContext.Current.Session[ConstValue.SessionKey_Identity] as Identity;
            }
        }

        public static bool Login(string code, string password, out string msg)
        {
            const string passwordSql = "select Id ,Name,Code, password from users where code = @code";
            Identity identity = DbQuery.New().ExecuteSingle<Identity>(passwordSql, new { code = code });
            if (identity == null)
            {
                msg = "用户名不存在";
                return false;
            }

            if (SecurityHelper.Sha1Encrypt(password) == identity.Password)
            {
                msg = string.Empty;
                HttpContext.Current.Session[ConstValue.SessionKey_Identity] = identity;
                return true;
            }
            else
            {
                msg = "用户名或密码错误";
                return false;
            }
        }
    }
}

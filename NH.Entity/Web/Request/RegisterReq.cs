using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web
{
    /// <summary>
    /// 用户注册请求类
    /// </summary>
    public class RegisterReq : RequestBase
    {
        /// <summary>
        /// 用户提交的手机号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 密码(MD5加密)
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
    }
}

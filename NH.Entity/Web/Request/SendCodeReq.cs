using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web
{
    /// <summary>
    /// 验证码请求实体
    /// </summary>
    public class SendCodeReq : RequestBase
    {
        /// <summary>
        /// 请求手机号码
        /// </summary>
        public string phone { get; set; }
    }
    /// <summary>
    /// 提交验证码
    /// </summary>
    public class SubmitCodeReq : RequestBase
    {
        /// <summary>
        /// 请求手机号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
    }
    /// <summary>
    /// 重置密码
    /// </summary>
    public class ResetPasswordReq : RequestBase
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}

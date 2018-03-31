using NH.Entity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Interface
{
    /// <summary>
    /// 生成验证码接口
    /// </summary>
    public interface IAuthVerifyDal
    {
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool CreateVerifyCode(string phone, string code);

       /// <summary>
        /// 提交验证码
        /// </summary>
        /// <param name="req"></param>
        /// <returns>1:成功；0：验证码过期；-1，用户不存在；-2：手机号码未注册</returns>
        int SubmitCode(SubmitCodeReq req);
    }
}

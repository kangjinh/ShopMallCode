using NH.Entity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Interface
{
    /// <summary>
    /// 登录注册接口
    /// </summary>
    public interface IAuthDao
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        int Regieste(RegisterReq mod);
        /// <summary>
        /// 用户名和密码登录
        /// </summary>
        /// <param name="username">type为0时username代表登录名，type为1时username代表手机号</param>
        /// <param name="password"></param>
        /// <returns></returns>
        int LoginUp(string username, string password, int type);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int ResetPassword(ResetPasswordReq req);
    }
}

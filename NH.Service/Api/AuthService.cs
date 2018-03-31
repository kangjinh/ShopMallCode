using NH.Entity.Web;
using NH.JQX.Data;
using NH.JQX.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Service.Api
{
    /// <summary>
    /// 注册与登录服务
    /// </summary>
    public class AuthService
    {
        private static object lockobject = new object();
        private IAuthDao dao
        {
            get
            {
                return new AuthDao();
            }
        }

        public static AuthService GetInstance()
        {
            if (_service==null)
            {
                lock(lockobject)
                {
                    if (_service==null)
                    {
                        _service = new AuthService();
                    }
                    return _service;
                }
            }
            return _service;
        }private static AuthService _service = null;

        #region 用户注册

        public virtual int Regieste(Entity.Web.RegisterReq entity)
        {
            return dao.Regieste(entity);
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户名和密码登录
        /// </summary>
        /// <param name="username">type为0时username代表登录名，type为1时username代表手机号</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual int LoginUp(string username, string password, int type)
        {
            return dao.LoginUp(username,password,type);            
        }

        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual int ResetPassword(ResetPasswordReq req)
        {
            return dao.ResetPassword(req);
        }
        #endregion 重置密码
    }
}

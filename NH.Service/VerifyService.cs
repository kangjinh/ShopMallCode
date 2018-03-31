using NH.Entity.Web;
using NH.JQX.Data;
using NH.JQX.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Service
{
    /// <summary>
    /// 验证码服务
    /// </summary>
    public class VerifyService
    {
        public static object lokob = new object();
        private IAuthVerifyDal dal
        {
            get { return new AuthVerifyDal(); }
        }

        public static VerifyService Instance()
        {
            if (_service ==null)
            {
                lock (lokob)
                {
                    if (_service==null)
                    {
                        _service = new VerifyService();
                    }
                }
            }
            return _service;
        }private static VerifyService _service = null;
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual bool CreateVerifyCode(string phone,string code)
        {
            return  dal.CreateVerifyCode(phone, code);
        }
        /// <summary>
        /// 提交验证码
        /// </summary>
        /// <param name="req"></param>
        /// <returns>1:成功；0：验证码过期；-1，用户不存在；-2：手机号码未注册</returns>
        public virtual int SubmitCode(SubmitCodeReq req)
        {
            return dal.SubmitCode(req);
        }
    }
}

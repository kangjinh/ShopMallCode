using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 生成手机验证码
    /// </summary>
    public class AuthVerify
    {

        public virtual int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public virtual string VerifyCode { get; set; }
        /// <summary>
        /// 过期时间（单位：分钟）
        /// </summary>
        public virtual int Expired { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public AuthVerify()
        {
            this.ID = 0;
            this.UserName = "";
            this.Phone = string.Empty;
            this.VerifyCode = string.Empty;
            this.Expired = 5;
            this.CreateTime = DateTime.Now;
        }
    }
}

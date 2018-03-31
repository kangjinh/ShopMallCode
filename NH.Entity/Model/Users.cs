using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual bool Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime Birthday { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 注册平台
        /// </summary>
        public virtual int Regplatform { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual int UserType { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime SaveTime { get; set; }
    }
}

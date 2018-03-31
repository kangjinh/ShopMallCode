using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 微信公众号账户实体类
    /// </summary>
    public class V2WeixinAccount
    {
        #region 私有变量

        /// <summary>
        /// 主键ID
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 微信名称
        /// </summary>
        private string _WeixinName = "";
        /// <summary>
        /// 微信账户
        /// </summary>
        private string _UserName = "";
        /// <summary>
        /// 微信账户登录密码
        /// </summary>
        private string _PassWord = "";
        /// <summary>
        /// 微信AppId
        /// </summary>
        private string _AppID = "";
        /// <summary>
        /// 微信AppSecret
        /// </summary>
        private string _AppSecret = "";
        /// <summary>
        /// 微信账户类型;对应枚举WeixinAccountType
        /// </summary>
        private int _AccountType = 0;
        /// <summary>
        /// 是否锁定
        /// </summary>
        private bool _IsLocked = false;
        /// <summary>
        /// 保存时间
        /// </summary>
        private DateTime _SaveTime = DateTime.Now;

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取或设置主键ID
        /// </summary>
        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 获取或设置微信名称
        /// </summary>
        public virtual string WeixinName
        {
            get { return _WeixinName; }
            set { _WeixinName = value; }
        }

        /// <summary>
        /// 获取或设置微信账户
        /// </summary>
        public virtual string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// 获取或设置微信账户对应的密码
        /// </summary>
        public virtual string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; }
        }

        /// <summary>
        /// 获取或设置微信AppId
        /// </summary>
        public virtual string AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        /// <summary>
        /// 获取或设置微信AppSecret
        /// </summary>
        public virtual string AppSecret
        {
            get { return _AppSecret; }
            set { _AppSecret = value; }
        }

        /// <summary>
        /// 获取或设置微信公众号类型;如(0-未知;1-订阅号;2-服务号),对应枚举WeixinAccountType
        /// </summary>
        public virtual int AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        /// <summary>
        /// 获取或设置是否锁定
        /// </summary>
        public virtual bool IsLocked
        {
            get { return _IsLocked; }
            set { _IsLocked = value; }
        }

        /// <summary>
        /// 获取或设置保存时间
        /// </summary>
        public virtual DateTime SaveTime
        {
            get { return _SaveTime; }
            set { _SaveTime = value; }
        }


        #endregion

        #region 附加字段

        /// <summary>
        /// 获取或设置截止时间
        /// </summary>
        //public DateTime EndTime { get; set; }

        #endregion



    }
}

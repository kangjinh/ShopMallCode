using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    public class Token
    {
        /// <summary>
        /// 获取或设置用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 获取或设置App密钥
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 获取或设置Token值是否过期
        /// </summary>
        public bool IsOverdue { get; set; }
        /// <summary>
        /// 获取或设置密钥是否有效
        /// </summary>
        public bool IsValidSecretKey { get; set; }
        /// <summary>
        /// token字符串
        /// </summary>
        public string TokenStr { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string Noncestr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 认证（0.未提交认证;1.已提交认证尚未认证;2.已提交认证但认证不通过;3.已提交认证并且已认证通过）
        /// </summary>
        public int Authenticate { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public Entity.EnumLibrary.Regplatform Platform { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 群聊消息
    /// </summary>
    public class GroupMsg
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 发消息者
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 发消息云信ID
        /// </summary>
        public virtual string YXID { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual int MsgType { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string MsgContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}

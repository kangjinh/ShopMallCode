using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 群聊成员
    /// </summary>
    public class GroupMember
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 群ID
        /// </summary>
        public virtual int GroupID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 未读数
        /// </summary>
        public virtual int UnReadCount { get; set; }

        public GroupMember()
        {
            this.ID = 0;
            this.GroupID = 0;
            this.UnReadCount = 0;
            this.UserID = 0;
        }
    }
}

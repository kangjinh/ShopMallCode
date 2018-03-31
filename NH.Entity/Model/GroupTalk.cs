using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 群聊类
    /// </summary>
    public class GroupTalk
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 群名字
        /// </summary>
        public virtual string GroupName { get; set; }
        /// <summary>
        /// 群主
        /// </summary>
        public virtual int Createor { get; set; }

        public GroupTalk()
        {
            this.ID = 0;
            this.GroupName = "";
            this.Createor = 0;
        }
    }
}

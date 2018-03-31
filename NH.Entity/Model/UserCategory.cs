using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 用户类目关系表--用户比较感兴趣的类目
    /// </summary>
    public class UserCategory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 类目ID
        /// </summary>
        public virtual int CategoryID { get; set; }

        public UserCategory()
        {
            this.ID = 0;
            this.UserID = 0;
            this.CategoryID = 0;
        }
    }
}

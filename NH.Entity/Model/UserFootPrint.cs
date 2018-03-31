using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 用户足迹(浏览商品历史记录)
    /// </summary>
    public class UserFootPrint
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int GoodsID { get; set; }
    }
}

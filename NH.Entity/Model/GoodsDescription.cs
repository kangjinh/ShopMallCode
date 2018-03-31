using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品详情描述表
    /// </summary>
    public class GoodsDescription
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int GoodsID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public GoodsDescription()
        {
            this.ID = 0;
            this.GoodsID = 0;
            this.Description = "";
        }
    }
}

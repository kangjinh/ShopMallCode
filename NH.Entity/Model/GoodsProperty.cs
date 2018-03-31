using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品属性表
    /// </summary>
    public class GoodsProperty
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 父属性ID
        /// </summary>
        public virtual int ParentID { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public virtual string PropertyName { get; set; }
        /// <summary>
        /// 类目ID
        /// </summary>
        public virtual int CategoryID { get; set; }
        /// <summary>
        /// 属性级别全路径
        /// </summary>
        public virtual string FullPath { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }

        public GoodsProperty()
        {
            this.ID = 0;
            this.IsLocked = false;
            this.FullPath = "";
            this.CategoryID = 0;
            this.PropertyName = "";
            this.ParentID = 0;
        }
    }
}

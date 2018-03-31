using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品类别
    /// </summary>
    public class BaseCategory
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public virtual int ParentID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public virtual string CategoryName { get; set; }
        /// <summary>
        /// 全路径用来查询层次关系
        /// </summary>
        public virtual string FullPath { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public virtual bool IsLeaf { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public BaseCategory()
        {
            this.ID = 0;
            this.ParentID = 0;
            this.CategoryName = string.Empty;
            this.FullPath = string.Empty;
            this.IsLocked = false;
            this.IsLeaf = false;
            this.CreateTime = DateTime.Now;
        }
    }
}

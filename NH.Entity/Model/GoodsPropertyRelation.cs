using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品属性关系表
    /// </summary>
    public class GoodsPropertyRelation
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int GoodsID { get; set; }
        /// <summary>
        /// 库存ID
        /// </summary>
        public virtual int StockID { get; set; }
        /// <summary>
        /// 属性ID
        /// </summary>
        public virtual int PropertyID { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public virtual string PropertyName { get; set; }
        /// <summary>
        /// 属性值ID
        /// </summary>
        public virtual int PropertyValueID { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public virtual string PropertyValue { get; set; }
    }
}

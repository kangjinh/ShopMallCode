using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品属性值表
    /// </summary>
    public class GoodsPropertyValue
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public virtual string PropertyValue { get; set; }
        /// <summary>
        /// 属性ID
        /// </summary>
        public virtual int PropertyID { get; set; }

    }
}

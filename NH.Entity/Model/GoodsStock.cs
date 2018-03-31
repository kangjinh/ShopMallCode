using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品SKU库存
    /// </summary>
    public class GoodsStock
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int GoodsID { get; set; }
        /// <summary>
        /// 商品属性字符串
        /// </summary>
        public virtual string SKUProperty { get; set; }
        /// <summary>
        /// 商品原价（以分为单位）
        /// </summary>
        public virtual int Price { get; set; }
        /// <summary>
        /// 商品真实价格（以分为单位）
        /// </summary>
        public virtual int ActualPrice { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public virtual int Stock { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public virtual int SalsCount { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }

        public GoodsStock()
        {
            this.ID = 0;
            this.IsLocked = false;
            this.SalsCount = 0;
            this.Stock = 0;
            this.ActualPrice = 0;
            this.Price = 0;
            this.SKUProperty = "";
            this.GoodsID = 0;
        }
    }
}

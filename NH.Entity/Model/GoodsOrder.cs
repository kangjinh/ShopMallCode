using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品订单表
    /// </summary>
    public class GoodsOrder
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 卖家ID
        /// </summary>
        public virtual int SalesID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int GoodsID { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// SKU库存ID
        /// </summary>
        public virtual string SKUID { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public virtual int OrderState { get; set; }
        /// <summary>
        /// 订单中购买商品数量
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 商品原价
        /// </summary>
        public virtual decimal Price { get; set; }
        /// <summary>
        /// 真实价格
        /// </summary>
        public virtual decimal ActualPrice { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 支付表ID
        /// </summary>
        public virtual int PayID { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public virtual decimal CouponAmount { get; set; }
        /// <summary>
        /// 用户付现金数
        /// </summary>
        public virtual decimal RealMoney { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDel { get; set; }
        /// <summary>
        /// 下订单时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单修改时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }
    }
}

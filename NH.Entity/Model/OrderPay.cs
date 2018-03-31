using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 订单支付表(购物车)
    /// </summary>
    public class OrderPay
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 用户ID(买家)
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 卖家ID
        /// </summary>
        public virtual int SalesID { get; set; }
        /// <summary>
        /// 支付金额(现金)
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否已支付
        /// </summary>
        public virtual bool IsPay { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual DateTime PayTime { get; set; }
        /// <summary>
        /// 是否有优惠券
        /// </summary>
        public virtual bool HasCoupon { get; set; }

        public OrderPay()
        {
            this.ID = 0;
            this.HasCoupon = false;
            this.PayTime = DateTime.Now;
            this.IsPay = false;
            this.CreateTime = DateTime.Now;
            this.IsLocked = false;
            this.Amount = 0;
            this.SalesID = 0;
            this.UserID = 0;
        }
    }
}

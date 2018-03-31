using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 订单支付明细
    /// </summary>
    public class OrderPayDetail
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 支付ID
        /// </summary>
        public virtual int PayID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public virtual int OrderID { get; set; }
        /// <summary>
        /// 支付类型：PayType枚举值
        /// </summary>
        public virtual int PayType { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 优惠券ID
        /// </summary>

        public virtual int CouponID { get; set; }

        public OrderPayDetail()
        {
            this.CouponID = 0;
            this.Amount = 0;
            this.PayType = 0;
            this.OrderID = 0;
            this.PayID = 0;
            this.ID = 0;
        }
     }
}

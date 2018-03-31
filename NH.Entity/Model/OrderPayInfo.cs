using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 支付订单单号和地址信息
    /// </summary>
    public class OrderPayInfo
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 支付订单ID
        /// </summary>
        public virtual int PayID { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        public virtual string PayNo { get; set; }
        /// <summary>
        /// 第三方订单号
        /// </summary>
        public virtual string OutPayNo { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        public OrderPayInfo()
        {
            this.ID = 0;
            this.PayID = 0;
            this.PayNo = "";
            this.OutPayNo = "";
            this.Address = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 订单状态改变历史
    /// </summary>
    public class OrderStateChangeHistory
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public virtual int OrderID { get; set; }
        /// <summary>
        /// 从哪个状态
        /// </summary>
        public virtual int FromState { get; set; }
        /// <summary>
        /// 转变的状态
        /// </summary>
        public virtual int ChangeState { get; set; }
        /// <summary>
        /// 转变时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public OrderStateChangeHistory()
        {
            this.ID = 0;
            this.OrderID = 0;
            this.FromState = 0;
            this.ChangeState = 0;
            this.CreateTime = DateTime.Now;
        }
    }
}

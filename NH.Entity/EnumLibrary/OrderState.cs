using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 商品订单
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [EnumShowName("待支付")]
        NoPay = 0,
        /// <summary>
        /// 待发货(已支付)
        /// </summary>
        [EnumShowName("待发货")]
        NoSendGoods = 1,
        /// <summary>
        /// 待收货(卖家已发货)
        /// </summary>
        [EnumShowName("待收货")]
        NoTakeGoods = 2,
        /// <summary>
        /// 待评价(买家已收货)
        /// </summary>
        [EnumShowName("待评价")]
        NoComment = 3,
        /// <summary>
        /// 已评价(订单已结束)
        /// </summary>
        [EnumShowName("已评价")]
        Comment = 4,
        /// <summary>
        /// 申请退货
        /// </summary>
        [EnumShowName("申请退货")]
        ApplicationBack = 5,
        /// <summary>
        /// 已退款
        /// </summary>
        [EnumShowName("已退款")]
        Refunded = 6
    }
}

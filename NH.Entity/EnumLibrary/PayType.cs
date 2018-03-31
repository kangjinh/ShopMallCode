using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 支付类型枚举
    /// </summary>
    public enum PayType
    {
        /// <summary>
        /// 普通商品
        /// </summary>
        [EnumShowName("未知")]
        UnKnow = 0,
        /// <summary>
        /// 团购
        /// </summary>
        [EnumShowName("支付宝")]
        AliPay = 1,
        /// <summary>
        /// 特卖
        /// </summary>
        [EnumShowName("微信")]
        WePay = 2,
        /// <summary>
        /// 展位
        /// </summary>
        [EnumShowName("PalPay")]
        PalPay = 3
    }
}

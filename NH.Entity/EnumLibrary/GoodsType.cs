using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 商品类型
    /// </summary>
    public enum GoodsType
    {
        /// <summary>
        /// 普通商品
        /// </summary>
        [EnumShowName("普通商品")]
        General = 0,
        /// <summary>
        /// 团购
        /// </summary>
        [EnumShowName("团购")]
        Guoup =1,
        /// <summary>
        /// 特卖
        /// </summary>
        [EnumShowName("特卖")]
        Special = 2,
        /// <summary>
        /// 展位
        /// </summary>
        [EnumShowName("展位")]
        Position = 3

    }
}

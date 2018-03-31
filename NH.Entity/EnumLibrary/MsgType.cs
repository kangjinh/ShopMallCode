using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        [EnumShowName("文本")]
        Text = 0,
        /// <summary>
        /// 图片
        /// </summary>
        [EnumShowName("图片")]
        Img = 1,
       
    }
}

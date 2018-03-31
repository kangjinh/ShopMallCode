using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.EnumLibrary
{
    /// <summary>
    /// 布尔值枚举
    /// </summary>
    public enum BoolValue
    {
        /// <summary>
        /// Flase
        /// </summary>
        [EnumShowName("False")]
        False = 0,
        /// <summary>
        /// True
        /// </summary>
        [EnumShowName("True")]
        True = 1
    }
}

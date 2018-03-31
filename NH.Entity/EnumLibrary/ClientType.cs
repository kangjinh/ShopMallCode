using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum ClientType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumShowName("未知")]
        UnKnown = 0,
        /// <summary>
        /// IOS
        /// </summary>
        [EnumShowName("IOS")]
        IOS = 1,
        /// <summary>
        /// Andriod
        /// </summary>
        [EnumShowName("Andriod")]
        Andriod = 2,
        /// <summary>
        /// PC
        /// </summary>
        [EnumShowName("PC")]
        PC = 3,
        /// <summary>
        /// WinPhone
        /// </summary>
        [EnumShowName("WinPhone")]
        WinPhone = 4,
        /// <summary>
        /// H5
        /// </summary>
        [EnumShowName("H5")]
        H5 = 5
    }
}

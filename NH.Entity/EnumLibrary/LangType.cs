using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 语言类型
    /// </summary>
    public enum LangType
    {
        /// <summary>
        /// 中文
        /// </summary>
        [EnumShowName("中文")]
        Chinese = 0,
        /// <summary>
        /// 英文
        /// </summary>
        [EnumShowName("英文")]
        English = 1
    }
}

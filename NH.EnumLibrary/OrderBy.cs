using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.EnumLibrary
{
    /// <summary>
    /// 字段排序枚举
    /// </summary>
    public enum OrderBy
    {
        /// <summary>
        /// 升序
        /// </summary>
        [EnumShowName("ASC")]
        ASC = 0,
        /// <summary>
        /// 降序
        /// </summary>
        [EnumShowName("DESC")]
        DESC = 1
    }
}

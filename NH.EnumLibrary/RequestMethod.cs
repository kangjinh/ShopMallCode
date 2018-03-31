using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.EnumLibrary
{
    /// <summary>
    /// 请求方式枚举
    /// </summary>
    public enum RequestMethod
    {
        /// <summary>
        /// GET请求方式
        /// </summary>
        [EnumShowName("GET")]
        GET = 0,
        /// <summary>
        /// POST请求方式
        /// </summary>
        [EnumShowName("POST")]
        POST = 1
    }
}

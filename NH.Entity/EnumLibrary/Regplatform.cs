using NH.EnumLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.EnumLibrary
{
    public enum Regplatform
    {
        /// <summary>
        /// 网站
        /// </summary>
        [EnumShowName("海淘网站")]
        Web = 0,
        /// <summary>
        /// 网站
        /// </summary>
        [EnumShowName("海淘APP")]
        APP = 1,
        /// <summary>
        /// 网站
        /// </summary>
        [EnumShowName("海淘H5")]
        H5 = 2
    }
}

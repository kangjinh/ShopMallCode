using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web
{
    /// <summary>
    /// 用户提交感兴趣的类目
    /// </summary>
    public class UserCategoryReq : RequestBase
    {
        //public int userid { get; set; }
        /// <summary>
        /// 感兴趣的类目--多项选择
        /// </summary>
        public string categoryids { get; set; }
    }
}

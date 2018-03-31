using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web.Request
{
    /// <summary>
    /// 用户首页上传地理位置
    /// </summary> 
    public class PositionReq :RequestBase
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double lat { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double lng { get; set; }
    }
}

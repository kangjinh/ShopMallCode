using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web
{
    /// <summary>
    /// 请求参数基类(公共参数)
    /// </summary>
    public class RequestBase
    {
        /// <summary>
        /// 平台类型 1:IOS; 2:Andriod; 5:H5
        /// </summary>
        public int client { get; set; }
        /// <summary>
        /// 语言 0:中文; 1:英文
        /// </summary>
        public int lang { get; set; }
        /// <summary>
        /// app版本号(接口版本号)
        /// </summary>
        public string appVersion { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        public string models { get; set; }
        /// <summary>
        /// 手机品牌，如Apple
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 手机系统版本
        /// </summary>
        public string systemVersion { get; set; }
        /// <summary>
        /// 用户Token
        /// </summary>
        public string token { get; set; }
    }
}

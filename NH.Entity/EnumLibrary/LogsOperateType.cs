using NH.EnumLibrary;
using System;


namespace NH.Entity.EnumLibrary
{
    /// <summary>
    /// 日志操作类型枚举
    /// </summary>
    public enum LogsOperateType
    {
        /// <summary>
        /// 登录
        /// </summary>
        [EnumShowName("登录")]
        Login = 1,
        /// <summary>
        /// 注册
        /// </summary>
        [EnumShowName("注册")]
        Register = 2
    }
}

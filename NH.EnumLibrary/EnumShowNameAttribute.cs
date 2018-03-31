using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.EnumLibrary
{
    /// <summary>
    /// 显示自定义枚举属性类
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumShowNameAttribute : Attribute
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        private string showName;
        /// <summary>
        /// 获取显示名称
        /// </summary>
        public string ShowName
        {
            get { return this.showName; }
        }
        /// <summary>
        /// 构造枚举的显示名称
        /// </summary>
        /// <param name="showName"></param>
        public EnumShowNameAttribute(string showName)
        {
            this.showName = showName;
        }
    }
}

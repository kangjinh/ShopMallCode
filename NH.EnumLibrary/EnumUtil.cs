using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace NH.EnumLibrary
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public sealed class EnumUtil
    {
        /// <summary>
        /// /枚举缓存池
        /// </summary>
        private static Dictionary<string, Dictionary<int, string>> _EnumList = new Dictionary<string, Dictionary<int, string>>();
        /// <summary>
        /// 枚举缓存池
        /// </summary>
        private static Dictionary<string, Dictionary<long, string>> _LEnumList = new Dictionary<string, Dictionary<long, string>>();
        /// <summary>
        /// 将枚举绑定到ListControl
        /// </summary>
        /// <param name="listControl">ListControl控件</param>
        /// <param name="enumType">枚举类型</param>
        public static void FillListControl(ListControl listControl, Type enumType)
        {
            listControl.Items.Clear();
            //listControl.DataSource = 
        }
        /// <summary>
        /// 将枚举转换成Dictionary<int,string>
        /// Dictionary中，key为枚举项对应的int值；value为：若定义了EnumShowName属性，则取它，否则取name
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(Type enumType)
        {
            string keyName = enumType.FullName;
            if (!_EnumList.ContainsKey(keyName))
            {
                Dictionary<int, string> list = new Dictionary<int, string>();
                foreach (int item in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, item);
                    //取显示名称
                    string showName = string.Empty;
                    object[] attribute = enumType.GetField(name).GetCustomAttributes(typeof(EnumShowNameAttribute), false);
                    if (attribute.Length > 0)
                        showName = ((EnumShowNameAttribute)attribute[0]).ShowName;
                    list.Add(item, string.IsNullOrEmpty(showName) ? name : showName);
                }
                object syncObj = new object();
                if (!_EnumList.ContainsKey(keyName))
                {
                    lock (syncObj)
                    {
                        if (!_EnumList.ContainsKey(keyName))
                        {
                            _EnumList.Add(keyName, list);
                        }
                    }
                }
            }
            return _EnumList[keyName];
        }
        /// <summary>
        /// 将枚举转换成Dictionary<int,string>
        /// Dictionary中，key为枚举项对应的int值；value为：若定义了EnumShowName属性，则取它，否则取name
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<long, string> LEnumToDictionary(Type enumType)
        {
            string keyName = enumType.FullName;
            if (!_LEnumList.ContainsKey(keyName))
            {
                Dictionary<long, string> list = new Dictionary<long, string>();
                foreach (long item in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, item);
                    //取得显示名称
                    string showName = string.Empty;
                    object[] attribute = enumType.GetField(name).GetCustomAttributes(typeof(EnumShowNameAttribute), false);
                    if (attribute.Length > 0)
                        showName = ((EnumShowNameAttribute)attribute[0]).ShowName;
                    list.Add(item, string.IsNullOrEmpty(showName) ? name : showName);
                }
                object syncObj = new object();
                if (!_LEnumList.ContainsKey(keyName))
                {
                    lock (syncObj)
                    {
                        if (!_LEnumList.ContainsKey(keyName))
                        {
                            _LEnumList.Add(keyName, list);
                        }
                    }
                }
            }
            return _LEnumList[keyName];
        }
        /// <summary>
        /// 获取枚举值对应的显示名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="intValue">枚举项对应的int值</param>
        /// <returns></returns>
        public static string GetEnumShowName(Type enumType, int intValue)
        {
            Dictionary<int, string> dict = EnumToDictionary(enumType);
            if (dict.ContainsKey(intValue))
            {
                return dict[intValue];
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取枚举值对应的显示名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="intValue">枚举项对应的int值</param>
        /// <returns></returns>
        public static string GetLEnumShowName(Type enumType, long intValue)
        {
            return LEnumToDictionary(enumType)[intValue];
        }

        /// <summary>
        /// 获取枚举对应的int值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEmum(ValueType value)
        {
            return (int)value;
        }









    }
}


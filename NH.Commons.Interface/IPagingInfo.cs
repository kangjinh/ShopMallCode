using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Commons.Interface
{
    /// <summary>
    /// 分页实体接口
    /// </summary>
    public interface IPagingInfo
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        int CurrentPage
        {
            get;
            set;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        long RecordCount
        {
            get;
            set;
        }
        /// <summary>
        /// 是否每次查询都计算总记录数
        /// </summary>
        bool CountEveryTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否计算记录数
        /// </summary>
        bool DoCount
        {
            get;
        }
        /// <summary>
        /// 是否合法
        /// </summary>
        bool IsLegal
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Web.Response
{
    /// <summary>
    /// BaseCategory的一级类目
    /// </summary>
    public class CategoryResponse
    {
        /// <summary>
        /// 类目ID
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string CategoryName{get;set;}
    }
}

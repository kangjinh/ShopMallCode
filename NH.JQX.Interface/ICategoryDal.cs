using NH.Entity.Web.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Interface
{
    public interface ICategoryDal
    {
        /// <summary>
        /// 获取一级类目列表
        /// </summary>
        /// <returns></returns>
        IList<CategoryResponse> GetParentCategoryList();

        /// <summary>
        /// 感兴趣的类目
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="categoryids"></param>
        /// <returns></returns>
        int InterstingCategory(int userid, int[] categoryids);
    }
}

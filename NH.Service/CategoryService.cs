using NH.Entity.Web.Response;
using NH.JQX.Data;
using NH.JQX.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Service
{
    public class CategoryService
    {
        private static object lockobj = new object();
        private ICategoryDal Dal
        {
            get { return new CategoryDal(); }
        }

        public static CategoryService Instance()
        {
            if (_service==null)
            {
                lock (lockobj)
                {
                    if (_service==null)
                    {
                        _service = new CategoryService();
                    }
                }
            }
            return _service;
        }private static CategoryService _service = null;

        #region 获取一级类目列表
        /// <summary>
        /// 获取一级类目列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<CategoryResponse> GetParentCategoryList()
        {
            return Dal.GetParentCategoryList();
        }
        #endregion

        #region 用户提交感兴趣的类目
        /// <summary>
        /// 感兴趣的类目
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="categoryids"></param>
        /// <returns></returns>
        public virtual int InterstingCategory(int userid,int[] categoryids)
        {
            return Dal.InterstingCategory(userid, categoryids);
        }
        #endregion
    }
}

using NH.Commons;
using NH.Commons.Data;
using NH.Entity.Model;
using NH.Entity.Web.Response;
using NH.JQX.Interface;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Data
{
    /// <summary>
    /// 类目操作
    /// </summary>
    public class CategoryDal : ICategoryDal
    {
        #region 获取一级类目列表
        /// <summary>
        /// 获取一级类目列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<CategoryResponse> GetParentCategoryList()
        {
            ISession Session = null;
            IList<CategoryResponse> list = null;
            try
            {
                
                Session = DBHelper.SessionFactory.OpenSession();
                var query = Session.QueryOver<BaseCategory>();
                var list1 = query.Where(c => c.ParentID == 0 && c.IsLocked == false).List();
                list = list1.Select(c => new CategoryResponse { CategoryId = c.ID, CategoryName = c.CategoryName }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("查询一级类目出错：" + ex.Message, "excep_CategoryDal.txt");
                return null;
            }
            finally
            {
                Session.Close();
                Session.Dispose();
            }
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
            if (userid>0)
            {
                ISession Session = null;
                ITransaction transaction = null;
                try
                {
                    Session = DBHelper.SessionFactory.OpenSession();
                    transaction = Session.BeginTransaction();
                    foreach (int categoryid in categoryids)
                    {
                        UserCategory item = new UserCategory();
                        item.UserID = userid;
                        item.CategoryID = categoryid;
                        Session.Save(item);
                    }                  
                    transaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog("用户提交感兴趣的类目保存出错：" + ex.Message, "Execption_Dal.txt");
                    return 0;
                }
                finally
                {
                    transaction.Dispose();
                    Session.Close();
                    Session.Dispose();
                }
            }
            return 0;
        }
        #endregion
    }
}

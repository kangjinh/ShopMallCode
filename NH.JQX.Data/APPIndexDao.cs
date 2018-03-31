using NH.Commons;
using NH.Commons.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NH.JQX.Data
{
    public class APPIndexDao
    {
        #region
        /// <summary>
        /// 获取用户首页推荐--APP版
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public DataSet GetIndexCommend(int userId,int pageindex,int pagesize=30)
        {
            DataBase db = new DataBase();
            try
            {
                SqlParameter[] prams = new SqlParameter[] {
                    db.MakeInParam("@userid", SqlDbType.Int, 4, userId),
                    db.MakeInParam("@pageindex", SqlDbType.Int, 4, pageindex),
                    db.MakeInParam("@pagesize", SqlDbType.Int, 4, pagesize),
                };
                DataSet ds = db.RunProcReturn("s3b_Api_GetIndexCommend", prams, 1);
                return ds;

            }
            catch(Exception ex)
            {
                LogHelper.WriteLog("获取用户首页推荐GetIndexCommend错误：" + ex.Message, "Exception_AppIndexDao");
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
            return null;
        }
        #endregion
    }
}

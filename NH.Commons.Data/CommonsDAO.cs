using NH.Commons.Interface;
using NH.EnumLibrary;
using NH.Utility.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace NH.Commons.Data
{
    /// <summary>
    /// 实现公共接口数据层
    /// </summary>
    public class CommonsDAO : ICommons
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private DataBase db = null;
        /// <summary>
        /// 字符串组合
        /// </summary>
        private StringBuilder builder = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonsDAO()
        {
            if (db == null)
                db = new DataBase();
            if (builder == null)
                builder = new StringBuilder();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConfig">数据库配置枚举</param>
        public CommonsDAO(DBConfig dbConfig)
        {
            if (db == null)
                db = new DataBase(dbConfig);
            if (builder == null)
                builder = new StringBuilder();
        }
        /// <summary>
        /// 清空指定表下指定字段的值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="IDString">需要清空数据的主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public bool ClearField(string tableName, string fieldName, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,100,fieldName),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_ClearFieldByIDString", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 清空指定表下指定字段的值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool ClearField(string tableName, string fieldName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,100,fieldName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_ClearFieldByID", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">
        /// SqlParameter[]类型数组
        /// 注意：SqlParameter[]参数数组中必须包含输出参数,存储过程中的参数要与之一致；
        /// 如：SqlParameter[] prams ={db.MakeInParam("@paramName",SqlDbType.VarChar,50,paramValue),db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)};
        /// </param>
        /// <returns>bool</returns>
        public bool ExecuteProcedure(string procName, SqlParameter[] prams)
        {
            try
            {
                db.RunProc(procName, prams);
                int reValue = Convert.ToInt16(prams[prams.Length - 1].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <returns>bool</returns>
        public bool ExecuteProce(string procName, SqlParameter[] prams)
        {
            return ExecuteProce(procName, prams, 0);
        }
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <param name="size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns>bool</returns>
        public bool ExecuteProce(string procName, SqlParameter[] prams, int size)
        {
            try
            {
                int rowsAffected = db.RunNonQuery(procName, prams, size);
                if (rowsAffected == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString(), "CommonsDAO_ExecuteProce.log");
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <returns>返回数据库的返回值,出错返回 500</returns>
        public int ExecProce(string procName, SqlParameter[] prams)
        {
            return ExecProce(procName, prams, 0);
        }
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <param name="size">存储过程和命令文本的识别参数，值1表示为命令文本</param>
        /// <returns>返回数据库的返回值,出错返回 500</returns>
        public int ExecProce(string procName, SqlParameter[] prams, int size)
        {
            try
            {
                return db.RunProc(procName, prams, size);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString(), "CommonsDAO_ExecProce.log");
                return 500;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 设置数据为解锁状态
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="isLocked">锁定或解锁(值只能是True或False)</param>
        /// <param name="IDString">需要锁定的数据主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public bool SetLockedOrUnLocked(string tableName, string isLocked, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@IsLocked",SqlDbType.VarChar,5,isLocked),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_SetLockedOrUnLocked", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 设置数据为解锁状态
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="isLocked">锁定或解锁(值只能是True或False)</param>
        /// <param name="IDString">需要锁定的数据主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public bool SetLockedOrUnLocked(string tableName, BoolValue isLocked, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@IsLocked",SqlDbType.VarChar,5,isLocked.ToString()),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_SetLockedOrUnLocked", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 删除指定主键的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool Delete(string tableName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_DeleteByID", prams);
                int reValue = Convert.ToInt16(prams[2].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 批量删除(ID主键组成的字符串)
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="IDString">ID主键组成的字符串</param>
        /// <returns></returns>
        public bool Delete(string tableName, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_DeleteByIDString", prams);
                int reValue = Convert.ToInt16(prams[2].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 删除指定字段下的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">指定字段名称</param>
        /// <param name="id">指定字段的值</param>
        /// <returns></returns>
        public bool Delete(string tableName, string fieldName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,30,fieldName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_DeleteByID_Extend", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 删除指定字段下的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">指定字段名称</param>
        /// <param name="IDString">指定字段的ID字符串(以,号链接在一起)</param>
        /// <returns></returns>
        public bool Delete(string tableName, string fieldName, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,30,fieldName),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_DeleteByIDString_Extend", prams);
                int reValue = Convert.ToInt16(prams[3].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldList">图像字段</param>
        /// <param name="IDString">要删除图片对应的主键ID字符串（以,号链接在一起）</param>
        /// <param name="isNeedClearFieldValue">是否需要清空字段值</param>
        /// </param>
        public void DeleteImage(string tableName, string fieldList, string IDString, bool isNeedClearFieldValue)
        {
            DataTable dt = GetFilterListByIDSring(tableName, fieldList, IDString);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string[] array = fieldList.Split(new Char[] { ',' });
                    //循环删除图片
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            //string url = System.Web.HttpContext.Current.Server.MapPath(@dr[array[i]].ToString());
                            string url = NH.Commons.ConfigHelper.GetConfigValueByKey("resource_physical_path") + @dr[array[i]].ToString();
                            if (File.Exists(url))
                            {
                                File.Delete(url);
                            }
                        }
                    }
                    if (isNeedClearFieldValue)
                    {
                        //清除数据库表中的图片字段值为空
                        for (int i = 0; i < array.Length; i++)
                        {
                            ClearField(tableName, array[i].ToString(), IDString);
                        }
                    }
                }
                dt.Clear();
                dt.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetListOrderBy<T>(string tableName, string orderByField, string orderBy)
        {
            DataTable dt = GetListOrderBy(tableName, orderByField, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetListOrderBy<T>(string tableName, string orderByField, OrderBy orderBy)
        {
            DataTable dt = GetListOrderBy(tableName, orderByField, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <returns></returns>
        public IList<T> GetTopList<T>(string tableName, int top)
        {
            DataTable dt = GetTopData(tableName, top);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public IList<T> GetTopList<T>(string tableName, int top, string filterWhere)
        {
            DataTable dt = GetTopData(tableName, top, filterWhere);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetTopList<T>(string tableName, int top, string filterWhere, string orderBy)
        {
            DataTable dt = GetTopData(tableName, top, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetTopList<T>(string tableName, int top, string filterWhere, OrderBy orderBy)
        {
            DataTable dt = GetTopData(tableName, top, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName)
        {
            DataTable dt = GetList(tableName);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName, string filterWhere)
        {
            DataTable dt = GetList(tableName, filterWhere);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName, string filterWhere, string orderBy)
        {
            DataTable dt = GetList(tableName, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName, string filterWhere, OrderBy orderBy)
        {
            DataTable dt = GetList(tableName, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName, int pageIndex, int pageSize, ref int rowsCount)
        {
            DataTable dt = GetDataTableByProcedure(pageIndex, pageSize, ref rowsCount, tableName);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取分页数据(含搜索)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string searchString, string tableName, int pageIndex, int pageSize, ref int rowsCount)
        {
            DataTable dt = GetDataTableByProcedure(pageIndex, pageSize, ref rowsCount, tableName, searchString);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string tableName, string filterWhere, string orderBy, int pageIndex, int pageSize, ref int rowsCount)
        {
            DataTable dt = GetPagerList(tableName, "*", orderBy, pageIndex, pageSize, filterWhere, ref rowsCount);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取指定要查询的信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldListString">图象字段列表,多个以逗号(,)连接在一起</param>
        /// <param name="IDString">ID主键组成的字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public IList<T> GetFilterListByIDSring<T>(string tableName, string fieldListString, string IDString)
        {
            DataTable dt = GetFilterListByIDSring(tableName, fieldListString, IDString);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <returns></returns>
        public IList<T> GetFilterList<T>(string tableName, string filterFieldString)
        {
            DataTable dt = GetFilterList(tableName, filterFieldString, "");
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <param name="filterWhere">筛选条件字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public IList<T> GetFilterList<T>(string tableName, string filterFieldString, string filterWhere)
        {
            DataTable dt = GetFilterList(tableName, filterFieldString, filterWhere);
            if (dt != null)
                return DataConvert.GetList<T>(dt);
            else
                return null;
        }
        /// <summary>
        /// 获取筛选排序列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起,如查全部字段,请使用*号</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0,注意,不能包含where关键字)</param>
        /// <param name="orderByString">排序语句(如:order by ID DESC或order by ID DESC,IsLocked ASC)</param>
        /// <returns></returns>
        public DataTable GetListFilterOrderBy(string tableName, string filterFieldString, string filterWhere, string orderByString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FilterFieldString",SqlDbType.VarChar,500,filterFieldString),
                db.MakeInParam("@FilterWhere",SqlDbType.VarChar,500,filterWhere),
                db.MakeInParam("@OrderByString",SqlDbType.VarChar,500,orderByString)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListFilterOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取指定要查询的信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public DataTable GetList(string tableName)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetList", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByWhere", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string filterWhere, string orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, int pageIndex, int pageSize, ref int rowsCount)
        {
            return GetDataTableByProcedure(pageIndex, pageSize, ref rowsCount, tableName);
        }
        /// <summary>
        /// 获取分页数据(含搜索)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public DataTable GetList(string searchString, string tableName, int pageIndex, int pageSize, ref int rowsCount)
        {
            return GetDataTableByProcedure(pageIndex, pageSize, ref rowsCount, tableName, searchString);
        }
        /// <summary>
        /// 获取高性能分页数据(含搜索)
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="fieldList">查询字段</param>
        /// <param name="orderName">排序字段(注意:适合自动增长主键或非重复主键,整型)</param>
        /// <param name="pageIndex">页索引,第一页为0</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBy">值0表示升序;值1表示降序</param>
        /// <param name="searchString">搜索语句,注意不包含where关健词</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string fieldList, string orderName, int pageIndex, int pageSize, OrderBy orderBy, string searchString, ref int rowsCount)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@tableName",SqlDbType.VarChar,255,tableName),
                db.MakeInParam("@strGetFields",SqlDbType.VarChar,1000,fieldList),
                db.MakeInParam("@OrderName",SqlDbType.VarChar,255,orderName),
                db.MakeInParam("@OrderType",SqlDbType.Bit,1,Convert.ToInt16(orderBy)),
                db.MakeInParam("@strWhere",SqlDbType.VarChar,1500,searchString),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,pageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,pageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetListPagerCommon", prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="sf">SqlFilter对象</param>
        /// <param name="searchString">搜索语句,注意不包含where关健词</param>
        /// <param name="pageIndex">页索引,第一页为0</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        public DataTable GetPagerList(SqlFilter sf, string searchString, int pageIndex, int pageSize, ref int rowsCount)
        {
            return GetList(sf.TableName, sf.FieldList, sf.OrderName, pageIndex, pageSize, sf.OrderBy, searchString, ref rowsCount);
        }
        /// <summary>
        /// 获取分页数据列表（自定义排序方式）
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldList">需要查询的字段</param>
        /// <param name="OrderStr">排序如： ID desc,name asc</param>
        /// <param name="PageIndex">页码，从0开始</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="WhereStr">条件</param>
        /// <param name="rowsCount">总数</param>
        /// <returns></returns>
        public DataTable GetPagerList(string TableName, string FieldList, string OrderStr, int PageIndex, int PageSize, string WhereStr, ref int rowsCount)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@tableName",SqlDbType.VarChar,255,TableName),
                db.MakeInParam("@ReFieldsStr",SqlDbType.VarChar,1000,FieldList),
                db.MakeInParam("@strWhere",SqlDbType.VarChar,500,WhereStr),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,500,OrderStr),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,PageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,PageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetListPagerCommonByOther", prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取fm统计详细信息列表
        /// </summary>
        /// <param name="FmName">fm名称</param>
        /// <param name="StarTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="PageIndex">当前页，从0开始算</param>
        /// <param name="PageSize">每页数量</param>
        /// <param name="rowsCount">总数</param>
        /// <returns></returns>
        public DataTable GetFmStatistics(string FmName, string OatherWhere, string StarTime, string EndTime, int PageIndex, int PageSize, ref int rowsCount, int AnchorId = 0)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@AnchorId",SqlDbType.Int,4,AnchorId),
                db.MakeInParam("@FmName",SqlDbType.VarChar,200,FmName),
                db.MakeInParam("@OatherWhere",SqlDbType.VarChar,200,OatherWhere),
                db.MakeInParam("@StarTime",SqlDbType.VarChar,30,StarTime),
                db.MakeInParam("@EndTime",SqlDbType.VarChar,30,EndTime),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,PageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,PageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetFmStatistics", prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }

        }
        /// <summary>
        /// 获取单条数据记录
        /// </summary>
        /// <param name="sf">SqlFilter对象</param>
        /// <param name="searchString">搜索语句,注意不包含where关健词</param>
        /// <returns></returns>
        public DataTable GetEntity(SqlFilter sf, string searchString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,255,sf.TableName),
                db.MakeInParam("@Fields",SqlDbType.VarChar,1000,sf.FieldList),
                db.MakeInParam("@Where",SqlDbType.VarChar,1500,searchString),
                db.MakeInParam("@OrderName",SqlDbType.VarChar,255,sf.OrderName),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,sf.OrderBy.ToString())
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetTopOneByWhere", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取2表内联查询
        /// </summary>
        /// <param name="tableName">数据库表名1(别名:a)</param>
        /// <param name="tableName2">数据库表名2(别名:b)</param>
        /// <param name="keyName">表1联合字段名</param>
        /// <param name="keyName2">表2联合字段名</param>
        /// <param name="columnNames">需查询的字段(所有为空字符)</param>
        /// <param name="filterWhere">条件</param>
        /// <returns></returns>
        public DataTable GetJoinList(string tableName, string tableName2, string keyName, string keyName2, string columnNames, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@tableName",SqlDbType.VarChar,60,tableName),
                db.MakeInParam("@tableName2",SqlDbType.VarChar,60,tableName2),
                db.MakeInParam("@keyName",SqlDbType.VarChar,50,keyName),
                db.MakeInParam("@keyName2",SqlDbType.VarChar,50,keyName2),
                db.MakeInParam("@columnNames",SqlDbType.VarChar,500,columnNames),
                db.MakeInParam("@filterWhere",SqlDbType.VarChar,500,filterWhere)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetListByInnerJoinTable", prams, 1);
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取存储过程分页数据(含搜索)
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public DataTable GetListByProcedure(int pageIndex, int pageSize, ref int rowsCount, string searchString, string procName)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@SearchString",SqlDbType.VarChar,1000,searchString),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,pageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,pageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn(procName, prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T GetEntity<T>(string tableName, int id)
        {
            DataTable dt = GetEntity(tableName, id);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询字段</param>
        /// <param name="id">查询字段的值</param>
        /// <returns></returns>
        public T GetEntity<T>(string tableName, string fieldName, int id)
        {
            DataTable dt = GetEntity(tableName, fieldName, id);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询字段</param>
        /// <param name="fieldValue">查询字段的值</param>
        /// <returns></returns>
        public T GetEntity<T>(string tableName, string fieldName, string fieldValue)
        {
            DataTable dt = GetEntity(tableName, fieldName, fieldValue);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public T GetTopOneEntity<T>(string tableName)
        {
            DataTable dt = GetTopOneData(tableName);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public T GetTopOneEntity<T>(string tableName, string filterWhere)
        {
            DataTable dt = GetTopOneData(tableName, filterWhere);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public T GetTopOneEntity<T>(string tableName, string filterWhere, string orderBy)
        {
            DataTable dt = GetTopOneData(tableName, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public T GetTopOneEntity<T>(string tableName, string filterWhere, OrderBy orderBy)
        {
            DataTable dt = GetTopOneData(tableName, filterWhere, orderBy);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 获取登录返回的登录用户实体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="accountField">登录账号对应的数据库表中的字段名称</param>
        /// <param name="passWordField">登录密码对应的数据库表中的字段名称</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="passsWord">登录密码</param>
        /// <returns></returns>
        public T Login<T>(string tableName, string accountField, string passWordField, string userName, string passsWord)
        {
            DataTable dt = GetLogin(tableName, accountField, passWordField, userName, passsWord);
            if (dt != null)
                return DataConvert.GetEntity<T>(dt);
            else
                return default(T);
        }
        /// <summary>
        /// 运行Sql语句(适合更新，删除，添加)
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int RunQuery(string strSql)
        {
            try
            {
                return db.RunQuery(strSql);
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取Select查询数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="selectQuery">Select语句</param>
        /// <returns></returns>
        public IList<T> GetListByQuery<T>(string selectQuery)
        {
            try
            {
                DataTable dt = db.RunTextReturn(DataCheck.SqlStringFilter(selectQuery)).Tables[0];
                if (dt != null)
                    return DataConvert.GetList<T>(dt);
                else
                    return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取Select查询数据
        /// </summary>
        /// <param name="selectQuery">Select语句</param>
        /// <returns></returns>
        public DataTable GetDataTableByQuery(string selectQuery)
        {
            try
            {
                return db.RunTextReturn(DataCheck.SqlStringFilter(selectQuery)).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 根据主键ID获取对应表中的相应值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="id">主键ID值</param>
        /// <returns></returns>
        public string GetFieldValue(string tableName, string fieldName, int id)
        {
            DataTable dt = GetObject(tableName, fieldName, id);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return Convert.ToString(dt.Rows[0][fieldName]);
                else
                    return null;
            else
                return null;
        }
        /// <summary>
        /// 根据主键ID获取对应表中的相应值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="filterWhere">筛选条件</param>
        /// <returns></returns>
        public string GetFieldValue(string tableName, string fieldName, string filterWhere)
        {
            DataTable dt = GetObject(tableName, fieldName, filterWhere);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return Convert.ToString(dt.Rows[0][fieldName]);
                else
                    return null;
            else
                return null;
        }
        /// <summary>
        /// 根据主键ID获取对应表中的相应名称
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="id">主键ID值</param>
        /// <returns></returns>
        public DataTable GetObject(string tableName, string fieldName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,50,fieldName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetObjectName", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 根据主键ID获取对应表中的相应名称
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="filterWhere">筛选条件</param>
        /// <returns></returns>
        public DataTable GetObject(string tableName, string fieldName, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,50,fieldName),
                db.MakeInParam("@FilterWhere",SqlDbType.VarChar,200,filterWhere)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetObjectValue", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取登录对象数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="accountField">登录账号对应的数据库表中的字段名称</param>
        /// <param name="passWordField">登录密码对应的数据库表中的字段名称</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="passsWord">登录密码</param>
        /// <returns></returns>
        public DataTable GetLogin(string tableName, string accountField, string passWordField, string userName, string passsWord)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@AccountField",SqlDbType.VarChar,20,accountField),
                db.MakeInParam("@PassWordField",SqlDbType.VarChar,20,passWordField),
                db.MakeInParam("@UserName",SqlDbType.VarChar,50,userName),
                db.MakeInParam("@Password",SqlDbType.VarChar,50,passsWord)
            };
            try
            {
                return db.RunProcReturn("sp_Public_Login", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 更改登录帐号
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="keyField">数据表主键字段名称</param>
        /// <param name="accountField">登录账号对应的数据库表中的字段名称</param>
        /// <param name="passWordField">登录密码对应的数据库表中的字段名称</param>
        /// <param name="keyID">用户ID(主键ID)</param>
        /// <param name="oldUserName">旧帐号</param>
        /// <param name="newUserName">新帐号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public bool ChangeLoginAccount(string tableName, string keyField, string accountField, string passWordField, int keyID, string oldUserName, string newUserName, string passWord)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@KeyField",SqlDbType.VarChar,20,keyField),
                db.MakeInParam("@AccountField",SqlDbType.VarChar,20,accountField),
                db.MakeInParam("@PassWordField",SqlDbType.VarChar,20,passWordField),
                db.MakeInParam("@KeyID",SqlDbType.Int,4,keyID),
                db.MakeInParam("@OldUserName",SqlDbType.VarChar,50,oldUserName),
                db.MakeInParam("@NewUserName",SqlDbType.VarChar,50,newUserName),
                db.MakeInParam("@PassWord",SqlDbType.VarChar,100,passWord),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_ChangeLoginAccount", prams);
                int reValue = Convert.ToInt16(prams[8].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 更改登录密码
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="keyField">数据表主键字段名称</param>
        /// <param name="passWordField">登录密码对应的数据库表中的字段名称</param>
        /// <param name="keyID">用户ID(主键)</param>
        /// <param name="oldPassWord">旧密码</param>
        /// <param name="newPassWord">新密码</param>
        /// <returns></returns>
        public bool ChangeLoginPassWord(string tableName, string keyField, string passWordField, int keyID, string oldPassWord, string newPassWord)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@KeyField",SqlDbType.VarChar,20,keyField),
                db.MakeInParam("@PassWordField",SqlDbType.VarChar,20,passWordField),
                db.MakeInParam("@KeyID",SqlDbType.Int,4,keyID),
                db.MakeInParam("@OldPassWord",SqlDbType.VarChar,100,oldPassWord),
                db.MakeInParam("@NewPassWord",SqlDbType.VarChar,100,newPassWord),
                db.MakeOutParam("@reValue",SqlDbType.TinyInt,1)
            };
            try
            {
                db.RunProc("sp_Public_ChangeLoginPassWord", prams);
                int reValue = Convert.ToInt16(prams[6].Value);
                if (reValue == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public DataTable GetTopOneData(string tableName)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetTopOne", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public DataTable GetTopOneData(string tableName, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetTopOneFromWhere", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public DataTable GetTopOneData(string tableName, string filterWhere, string orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetTopOneFromWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序枚举(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetTopOneData(string tableName, string filterWhere, OrderBy orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy.ToString())
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetTopOneFromWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public DataTable GetEntity(string tableName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetEntityByID", prams, 1).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询的字段名称</param>
        /// <param name="id">查询的字段值</param>
        /// <returns></returns>
        public DataTable GetEntity(string tableName, string fieldName, int id)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,30,fieldName),
                db.MakeInParam("@ID",SqlDbType.Int,4,id)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetEntityByID_Extend", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询的字段名称</param>
        /// <param name="fieldValue">查询的字段值</param>
        /// <returns></returns>
        public DataTable GetEntity(string tableName, string fieldName, string fieldValue)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,30,fieldName),
                db.MakeInParam("@FieldValue",SqlDbType.VarChar,50,fieldValue)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetEntityByString", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序枚举(只能为ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetListOrderBy(string tableName, string orderByField, OrderBy orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@OrderByField",SqlDbType.VarChar,50,orderByField),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy.ToString())
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetListOrderBy(string tableName, string orderByField, string orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@OrderByField",SqlDbType.VarChar,50,orderByField),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取指定要查询的字段信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldListString">字段列表,多个以逗号(,)连接在一起</param>
        /// <param name="IDString">ID主键组成的字符串</param>
        /// <returns></returns>
        public DataTable GetFilterListByIDSring(string tableName, string fieldListString, string IDString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldListString",SqlDbType.VarChar,1000,fieldListString),
                db.MakeInParam("@IDString",SqlDbType.VarChar,500,IDString)
            };
            try
            {
                return db.RunProcReturn("sp_Public_ReadByIDString", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <returns></returns>
        public DataTable GetFilterList(string tableName, string filterFieldString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FilterFieldString",SqlDbType.VarChar,300,filterFieldString),
                db.MakeInParam("@FilterWhere",SqlDbType.VarChar,300,"")
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetFilterList", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <param name="filterWhere">筛选条件字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public DataTable GetFilterList(string tableName, string filterFieldString, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FilterFieldString",SqlDbType.VarChar,300,filterFieldString),
                db.MakeInParam("@FilterWhere",SqlDbType.VarChar,300,filterWhere)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetFilterList", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取筛选数量
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件字符串（以,号链接在一起）</param>
        /// <returns></returns>
        public int GetFilterCount(string tableName, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere)
            };

            try
            {
                int count = 0;
                object value = db.RunScalar("sp_Public_GetCountByWhere", prams);
                if (value != null)
                {
                    count = int.Parse(value.ToString());
                }
                return count;
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取字符Sum的计算数值
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public decimal GetFieldSum(string tableName, string fieldName)
        {
            return GetFieldSum(tableName, fieldName, "");
        }
        /// <summary>
        /// 获取字符Sum的计算数值
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="filterWhere">筛选条件字符串（条件非空，自动在包含where关健字，不需要另外）</param>
        /// <returns></returns>
        public decimal GetFieldSum(string tableName, string fieldName, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@FieldName",SqlDbType.VarChar,50, fieldName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere)
            };

            try
            {
                decimal sum = 0;
                object value = db.RunScalar("sp_Public_GetSumByWhere", prams);
                if (value != null)
                {
                    sum = Convert.ToDecimal(value);
                }
                return sum;
            }
            catch
            {
                return 0;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <returns></returns>
        public DataTable GetTopData(string tableName, int top)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Top",SqlDbType.Int,4,top)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByTop", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        public DataTable GetTopData(string tableName, int top, string filterWhere)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Top",SqlDbType.Int,4,top),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByTopFromWhere", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序枚举(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetTopData(string tableName, int top, string filterWhere, string orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Top",SqlDbType.Int,4,top),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy)
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByTopFromWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取前top条记录
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">top条记录</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetTopData(string tableName, int top, string filterWhere, OrderBy orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Top",SqlDbType.Int,4,top),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy.ToString())
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByTopFromWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序枚举(值只能是ASC或DESC)</param>
        /// <returns></returns>
        public DataTable GetList(string tableName, string filterWhere, OrderBy orderBy)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@Where",SqlDbType.VarChar,1000,filterWhere),
                db.MakeInParam("@OrderBy",SqlDbType.VarChar,4,orderBy.ToString())
            };
            try
            {
                return db.RunProcReturn("sp_Public_GetListByWhereAndOrderBy", prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public DataTable GetDataTableByProcedure(int pageIndex, int pageSize, ref int rowsCount, string tableName)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,pageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,pageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetListPager", prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <returns></returns>
        public DataTable GetDataTableByProcedure(int pageIndex, int pageSize, ref int rowsCount, string tableName, string searchString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@SearchString",SqlDbType.VarChar,1000,searchString),
                db.MakeInParam("@TableName",SqlDbType.VarChar,50,tableName),
                db.MakeInParam("@PageIndex",SqlDbType.Int,4,pageIndex),
                db.MakeInParam("@PageSize",SqlDbType.Int,4,pageSize)
            };
            try
            {
                DataSet ds = db.RunProcReturn("sp_Public_GetSearchListPager", prams, 1);
                rowsCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[1];
                return dt;
            }
            catch
            {
                rowsCount = 0;
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取导入数据的数据源
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索条件</param>
        /// <returns></returns>
        public DataTable GetNoPagerDataSource(string sProcName, string searchString)
        {
            SqlParameter[] prams =
            {
                db.MakeInParam("@SearchString",SqlDbType.VarChar,1000,searchString)
            };
            try
            {
                return db.RunProcReturn(sProcName, prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索键值字符串</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <returns></returns>
        public DataTable GetDataTableByProcedure(string sProcName)
        {
            DataBase db = new DataBase();
            try
            {
                return db.RunProcReturn(sProcName, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索键值字符串</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <returns></returns>
        public DataTable GetDataTableByProcedure(string sProcName, SqlParameter[] prams)
        {
            DataBase db = new DataBase();
            try
            {
                return db.RunProcReturn(sProcName, prams, 1).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                db.Close();
                db.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        public void ShowTree(DataTable dt, TreeView tv, bool isGetValue)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(tv, 0, (TreeNode)null, dt, isGetValue);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        public void ShowTree(DataTable dt, TreeView tv, bool isGetValue, string nodeFieldName)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(tv, 0, (TreeNode)null, dt, isGetValue, nodeFieldName);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        public void ShowTree(DataTable dt, TreeView tv, string url)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(tv, 0, (TreeNode)null, dt, url);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        public void ShowTree(DataTable dt, TreeView tv, string url, string nodeFieldName)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(tv, 0, (TreeNode)null, dt, url, nodeFieldName);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        public void ShowTree(DataTable dt, string functionName, TreeView tv)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(functionName, tv, 0, (TreeNode)null, dt);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        public void ShowTree(DataTable dt, string functionName, TreeView tv, string nodeFieldName)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(functionName, tv, 0, (TreeNode)null, dt, nodeFieldName);
                dt.Dispose();
            }
        }

        public void ShowTree(DataTable dt, string functionName, string nodeFieldName, TreeView tv)
        {
            tv.Nodes.Clear();
            if (dt != null)
            {
                this.LoadTree(functionName, tv, -1, (TreeNode)null, dt, nodeFieldName);
                dt.Dispose();
            }
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="dt">树数据源</param>
        /// <param name="subdt">树子数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        /// <param name="subNodeFieldName">节点名称对应的数据表字段名称</param>
        public void ShowTree(DataTable dt, DataTable subdt, TreeView tv, string url, string nodeFieldName, string subNodeFieldName)
        {
            tv.Nodes.Clear();
            if (dt != null && subdt != null)
            {
                this.LoadTree(tv, 0, (TreeNode)null, dt, subdt, url, nodeFieldName, subNodeFieldName);
                dt.Dispose();
                subdt.Dispose();
            }
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        private void LoadTree(TreeView tv, int parentID, TreeNode pNode, DataTable dt, bool isGetValue)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                if (isGetValue)
                    tn.Text = "<span onclick=\"javascript:getNode('" + drv["NodeName"].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv["NodeName"].ToString() + "</span>";
                else
                    tn.Text = Convert.ToString(drv["NodeName"]);
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv["NodeName"].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, isGetValue); //再次递归
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        private void LoadTree(TreeView tv, int parentID, TreeNode pNode, DataTable dt, bool isGetValue, string nodeFieldName)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                if (isGetValue)
                    tn.Text = "<span onclick=\"javascript:getNode('" + drv[nodeFieldName].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv[nodeFieldName].ToString() + "</span>";
                else
                    tn.Text = Convert.ToString(drv[nodeFieldName]);
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv[nodeFieldName].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, isGetValue, nodeFieldName); //再次递归
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="url">链接地址</param>
        private void LoadTree(TreeView tv, int parentID, TreeNode pNode, DataTable dt, string url)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                if (url == null)
                    tn.Text = "<span onclick=\"javascript:getNode('" + drv["NodeName"].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv["NodeName"].ToString() + "</span>";
                else
                    tn.Text = Convert.ToString(drv["NodeName"]) + "&nbsp;&nbsp;<a href=\"" + url + "?id=" + Convert.ToString(drv["ID"]) + "\"><span style=\"color:red\">[修改]</span></a>";
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv["NodeName"].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, url); //再次递归
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        private void LoadTree(TreeView tv, int parentID, TreeNode pNode, DataTable dt, string url, string nodeFieldName)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                if (url == null)
                    tn.Text = "<span onclick=\"javascript:getNode('" + drv[nodeFieldName].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv[nodeFieldName].ToString() + "</span>";
                else
                    tn.Text = Convert.ToString(drv[nodeFieldName]) + "&nbsp;&nbsp;<a href=\"" + url + "?id=" + Convert.ToString(drv["ID"]) + "\"><span style=\"color:red\">[修改]</span></a>";
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv[nodeFieldName].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, url, nodeFieldName); //再次递归
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        private void LoadTree(string functionName, TreeView tv, int parentID, TreeNode pNode, DataTable dt)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                tn.Text = "<span onclick=\"javascript:" + functionName + "('" + drv["NodeName"].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv["NodeName"].ToString() + "</span>";
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv["NodeName"].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(functionName, tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt); //再次递归
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        private void LoadTree(string functionName, TreeView tv, int parentID, TreeNode pNode, DataTable dt, string nodeFieldName)
        {
            DataView dv = new DataView(dt);
            if (parentID >= 0)
            {
                //过滤ParentID,得到当前的所有子节点
                dv.RowFilter = "[ParentID] = " + parentID.ToString();
            }
            foreach (DataRowView drv in dv)
            {
                TreeNode tn = new TreeNode();
                tn.Text = "<span onclick=\"javascript:" + functionName + "('" + drv[nodeFieldName].ToString() + "','" + drv["ID"].ToString() + "')\" style=\"cursor:pointer\">" + drv["ID"].ToString() + " - " + drv[nodeFieldName].ToString() + "</span>";
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv[nodeFieldName].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                if ((parentID >= 0))
                {
                    LoadTree(functionName, tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, nodeFieldName); //再次递归
                }
            }
            dv.Dispose();
        }
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="tv">TreeView控件</param>
        /// <param name="parentID">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="dt">树数据源</param>
        /// <param name="subdt">树子数据源</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        /// <param name="subNodeFieldName">节点名称对应的数据表字段名称</param>
        private void LoadTree(TreeView tv, int parentID, TreeNode pNode, DataTable dt, DataTable subdt, string url, string nodeFieldName, string subNodeFieldName)
        {
            DataView subdv = null;
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点
            dv.RowFilter = "[ParentID] = " + parentID.ToString();
            foreach (DataRowView drv in dv)
            {
                subdv = new DataView(subdt);
                builder.Length = 0;
                //过滤TagID,得到对应的数据
                subdv.RowFilter = "[TagID] = " + drv["ID"].ToString();
                foreach (DataRowView subdrv in subdv)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(subdrv[subNodeFieldName].ToString());
                }
                if (builder.Length > 0)
                {
                    builder.Insert(0, "　<a style=\"color:blue;text-decoration: none;\">(");
                    builder.Append(")</a>");
                }

                TreeNode tn = new TreeNode();
                if (url == null)
                {
                    tn.Text = string.Format("<span onclick=\"javascript:getNode('{0}','{1}')\" style=\"cursor:pointer\">{1} - {2}</span>{3}",
                         drv[nodeFieldName].ToString(), drv["ID"].ToString(), drv[nodeFieldName].ToString(), builder.ToString());
                }
                else
                {
                    tn.Text = string.Format("{2} - {0}{3}　<a href=\"{1}?id={2}\"><span style=\"color:red\">[修改]</span></a>",
                        Convert.ToString(drv[nodeFieldName]), url, Convert.ToString(drv["ID"]), builder.ToString());
                }
                tn.Value = drv["ID"].ToString();
                tn.ImageToolTip = drv[nodeFieldName].ToString();
                tn.SelectAction = TreeNodeSelectAction.Expand;
                if (pNode == null)
                    tv.Nodes.Add(tn);   //添加根节点
                else
                    pNode.ChildNodes.Add(tn);   //添加当前节点的子节点
                //tv.Nodes[0].Expand();
                LoadTree(tv, Convert.ToInt32(drv["ID"].ToString()), tn, dt, subdt, url, nodeFieldName, subNodeFieldName); //再次递归
            }
            dv.Dispose();
        }

    }
}

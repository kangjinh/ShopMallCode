using NH.EnumLibrary;
using NH.Utility.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace NH.Commons.Interface
{
    /// <summary>
    /// 公共数据接口(适合ADO模式)
    /// </summary>
    public interface ICommons
    {
        /// <summary>
        /// 清空指定表下指定字段的值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="IDString">需要清空数据的主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        bool ClearField(string tableName, string fieldName, string IDString);
        /// <summary>
        /// 清空指定表下指定字段的值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool ClearField(string tableName, string fieldName, int id);
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
        bool ExecuteProcedure(string procName, SqlParameter[] prams);
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <returns>bool</returns>
        bool ExecuteProce(string procName, SqlParameter[] prams);
        /// <summary>
        /// 执定指定存储过程操作
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="prams">SqlParameter[]类型数组</param>
        /// <returns>返回数据库的返回值,出错返回 500</returns>
        int ExecProce(string procName, SqlParameter[] prams);
        /// <summary>
        /// 设置数据为解锁状态
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="isLocked">锁定或解锁(值只能是True或False)</param>
        /// <param name="IDString">需要锁定的数据主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        bool SetLockedOrUnLocked(string tableName, string isLocked, string IDString);
        /// <summary>
        /// 设置数据为解锁状态
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="isLocked">锁定或解锁(值只能是True或False)</param>
        /// <param name="IDString">需要锁定的数据主键ID字符串（以,号链接在一起）</param>
        /// <returns></returns>
        bool SetLockedOrUnLocked(string tableName, BoolValue isLocked, string IDString);
        /// <summary>
        /// 删除指定主键的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool Delete(string tableName, int id);
        /// <summary>
        /// 批量删除(ID主键组成的字符串)
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="IDString">ID主键组成的字符串</param>
        /// <returns></returns>
        bool Delete(string tableName, string IDString);
        /// <summary>
        /// 删除指定字段下的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">指定字段名称</param>
        /// <param name="id">指定字段的值</param>
        /// <returns></returns>
        bool Delete(string tableName, string fieldName, int id);
        /// <summary>
        /// 删除指定字段下的数据
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">指定字段名称</param>
        /// <param name="IDString">指定字段的ID字符串(以,号链接在一起)</param>
        /// <returns></returns>
        bool Delete(string tableName, string fieldName, string IDString);
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldList">图像字段</param>
        /// <param name="IDString">要删除图片对应的主键ID字符串（以,号链接在一起）</param>
        /// <param name="filePath">存放文件的相对路径(如 /upfiles/advert/test.jpg)</param>
        /// <param name="isNeedClearFieldValue">是否需要清空字段值</param>
        void DeleteImage(string tableName, string fieldList, string IDString, bool isNeedClearFieldValue);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetListOrderBy<T>(string tableName, string orderByField, string orderBy);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetListOrderBy<T>(string tableName, string orderByField, OrderBy orderBy);
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <returns></returns>
        IList<T> GetTopList<T>(string tableName, int top);
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        IList<T> GetTopList<T>(string tableName, int top, string filterWhere);
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetTopList<T>(string tableName, int top, string filterWhere, string orderBy);
        /// <summary>
        /// 获取前Top条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetTopList<T>(string tableName, int top, string filterWhere, OrderBy orderBy);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName, string filterWhere);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName, string filterWhere, string orderBy);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName, string filterWhere, OrderBy orderBy);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName, int pageIndex, int pageSize, ref int rowsCount);
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
        IList<T> GetList<T>(string searchString, string tableName, int pageIndex, int pageSize, ref int rowsCount);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        IList<T> GetList<T>(string tableName, string filterWhere, string orderBy, int pageIndex, int pageSize, ref int rowsCount);
        /// <summary>
        /// 获取指定要查询的信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldListString">图象字段列表,多个以逗号(,)连接在一起</param>
        /// <param name="IDString">ID主键组成的字符串（以,号链接在一起）</param>
        /// <returns></returns>
        IList<T> GetFilterListByIDSring<T>(string tableName, string fieldListString, string IDString);
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <returns></returns>
        IList<T> GetFilterList<T>(string tableName, string filterFieldString);
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <param name="filterWhere">筛选条件字符串（以,号链接在一起）</param>
        /// <returns></returns>
        IList<T> GetFilterList<T>(string tableName, string filterFieldString, string filterWhere);
        /// <summary>
        /// 获取筛选排序列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起,如查全部字段,请使用*号</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0,注意,不能包含where关键字)</param>
        /// <param name="orderByString">排序语句(如:order by ID DESC或order by ID DESC,IsLocked ASC)</param>
        /// <returns></returns>
        DataTable GetListFilterOrderBy(string tableName, string filterFieldString, string filterWhere, string orderByString);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        DataTable GetListOrderBy(string tableName, string orderByField, string orderBy);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="orderByField">排序字段名称</param>
        /// <param name="orderBy">排序方式(只能为ASC或DESC)</param>
        /// <returns></returns>
        DataTable GetListOrderBy(string tableName, string orderByField, OrderBy orderBy);
        /// <summary>
        /// 获取指定要查询的信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        DataTable GetList(string tableName);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        DataTable GetList(string tableName, string filterWhere);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0,注意,不能包含where关键字)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        DataTable GetList(string tableName, string filterWhere, string orderBy);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        DataTable GetList(string tableName, int pageIndex, int pageSize, ref int rowsCount);
        /// <summary>
        /// 获取分页数据(含搜索)
        /// </summary>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        DataTable GetList(string searchString, string tableName, int pageIndex, int pageSize, ref int rowsCount);
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
        DataTable GetList(string tableName, string fieldList, string orderName, int pageIndex, int pageSize, OrderBy orderBy, string searchString, ref int rowsCount);
        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="sf">SqlFilter对象</param>
        /// <param name="searchString">搜索语句,注意不包含where关健词</param>
        /// <param name="pageIndex">页索引,第一页为0</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <returns></returns>
        DataTable GetPagerList(SqlFilter sf, string searchString, int pageIndex, int pageSize, ref int rowsCount);
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
        DataTable GetPagerList(string TableName, string FieldList, string OrderStr, int PageIndex, int PageSize, string WhereStr, ref int rowsCount);
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
        DataTable GetFmStatistics(string FmName, string OatherWhere, string StarTime, string EndTime, int PageIndex, int PageSize, ref int rowsCount, int AnchorId = 0);
        /// <summary>
        /// 获取单条数据记录
        /// </summary>
        /// <param name="sf">SqlFilter对象</param>
        /// <param name="searchString">搜索语句,注意不包含where关健词</param>
        /// <returns></returns>
        DataTable GetEntity(SqlFilter sf, string searchString);
        /// <summary>
        /// 获取筛选数量
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件字符串（条件非空，自动在包含where关健字，不需要另外）</param>
        /// <returns></returns>
        int GetFilterCount(string tableName, string filterWhere);
        /// <summary>
        /// 获取字符Sum的计算数值
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        decimal GetFieldSum(string tableName, string fieldName);
        /// <summary>
        /// 获取字符Sum的计算数值
        /// </summary>
        /// <param name="tableName">数据库表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="filterWhere">筛选条件字符串（条件非空，自动在包含where关健字，不需要另外）</param>
        /// <returns></returns>
        decimal GetFieldSum(string tableName, string fieldName, string filterWhere);
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
        DataTable GetJoinList(string tableName, string tableName2, string keyName, string keyName2, string columnNames, string filterWhere);
        /// <summary>
        /// 获取存储过程分页数据(含搜索)
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总记录量</param>
        /// <param name="searchString">搜索键值字符(即SQL语句Where后面的字段查询条件)</param>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        DataTable GetListByProcedure(int pageIndex, int pageSize, ref int rowsCount, string searchString, string procName);
        /// <summary>
        /// 获取导入数据的数据源
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索条件</param>
        /// <returns></returns>
        DataTable GetNoPagerDataSource(string sProcName, string searchString);
        /// <summary>
        /// 获取指定要查询的信息
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldListString">图象字段列表,多个以逗号(,)连接在一起</param>
        /// <param name="IDString">ID主键组成的字符串（以,号链接在一起）</param>
        /// <returns></returns>
        DataTable GetFilterListByIDSring(string tableName, string fieldListString, string IDString);
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <returns></returns>
        DataTable GetFilterList(string tableName, string filterFieldString);
        /// <summary>
        /// 获取筛选列表
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterFieldString">筛选字段列表,多个以逗号(,)链接在一起</param>
        /// <param name="filterWhere">筛选条件字符串（以,号链接在一起）</param>
        /// <returns></returns>
        DataTable GetFilterList(string tableName, string filterFieldString, string filterWhere);
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetEntity<T>(string tableName, int id);
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询字段</param>
        /// <param name="id">查询字段的值</param>
        /// <returns></returns>
        T GetEntity<T>(string tableName, string fieldName, int id);
        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查询字段</param>
        /// <param name="fieldValue">查询字段的值</param>
        /// <returns></returns>
        T GetEntity<T>(string tableName, string fieldName, string fieldValue);
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        T GetTopOneEntity<T>(string tableName);
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="top">前top条</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <returns></returns>
        T GetTopOneEntity<T>(string tableName, string filterWhere);
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        T GetTopOneEntity<T>(string tableName, string filterWhere, string orderBy);
        /// <summary>
        /// 获取前1条数据列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="tableName">数据库表名</param>
        /// <param name="filterWhere">筛选条件(如：IsHotProduct = 1 and IsLocked = 0)</param>
        /// <param name="orderBy">排序(值只能是ASC或DESC)</param>
        /// <returns></returns>
        T GetTopOneEntity<T>(string tableName, string filterWhere, OrderBy orderBy);
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
        T Login<T>(string tableName, string accountField, string passWordField, string userName, string passsWord);
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
        bool ChangeLoginAccount(string tableName, string keyField, string accountField, string passWordField, int keyID, string oldUserName, string newUserName, string passWord);
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
        bool ChangeLoginPassWord(string tableName, string keyField, string passWordField, int keyID, string oldPassWord, string newPassWord);
        /// <summary>
        /// 运行Sql语句(适合更新，删除，添加)
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        int RunQuery(string strSql);
        /// <summary>
        /// 获取Select查询数据(注意，select语句中的字段不能使用别名，不能实体类找不到会报错)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="selectQuery">Select语句</param>
        /// <returns></returns>
        IList<T> GetListByQuery<T>(string selectQuery);
        /// <summary>
        /// 获取Select查询数据(注意：select语句中可以使用别名)
        /// </summary>
        /// <param name="selectQuery">Select语句</param>
        /// <returns></returns>
        DataTable GetDataTableByQuery(string selectQuery);
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索键值字符串</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <returns></returns>
        DataTable GetDataTableByProcedure(string sProcName);
        /// <summary>
        /// 获取所有的记录数据
        /// </summary>
        /// <param name="sProcName">存储过程名称</param>
        /// <param name="searchString">搜索键值字符串</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="rowsCount">数据总行数</param>
        /// <returns></returns>
        DataTable GetDataTableByProcedure(string sProcName, SqlParameter[] prams);
        /// <summary>
        /// 根据主键ID获取对应表中的相应名称
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="id">主键ID值</param>
        /// <returns></returns>
        DataTable GetObject(string tableName, string fieldName, int id);
        /// <summary>
        /// 根据主键ID获取对应表中的相应名称
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="filterWhere">筛选条件</param>
        /// <returns></returns>
        DataTable GetObject(string tableName, string fieldName, string filterWhere);
        /// <summary>
        /// 根据主键ID获取对应表中的相应值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="id">主键ID值</param>
        /// <returns></returns>
        string GetFieldValue(string tableName, string fieldName, int id);
        /// <summary>
        /// 根据主键ID获取对应表中的相应值
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="fieldName">查找的字段名称</param>
        /// <param name="filterWhere">筛选条件</param>
        /// <returns></returns>
        string GetFieldValue(string tableName, string fieldName, string filterWhere);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        void ShowTree(DataTable dt, TreeView tv, bool isGetValue);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="isGetValue">是否需要取得节点值和显示文本</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        void ShowTree(DataTable dt, TreeView tv, bool isGetValue, string nodeFieldName);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        void ShowTree(DataTable dt, TreeView tv, string url);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        void ShowTree(DataTable dt, TreeView tv, string url, string nodeFieldName);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        void ShowTree(DataTable dt, string functionName, TreeView tv);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        void ShowTree(DataTable dt, string functionName, TreeView tv, string nodeFieldName);
        /// <summary>
        /// 显示部门树
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="functionName">javascript函数名</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        void ShowTree(DataTable dt, string functionName, string nodeFieldName, TreeView tv);
        /// <summary>
        /// 构造树数据
        /// </summary>
        /// <param name="dt">树数据源</param>
        /// <param name="subdt">树子数据源</param>
        /// <param name="tv">TreeView控件</param>
        /// <param name="url">链接地址</param>
        /// <param name="nodeFieldName">节点名称对应的数据表字段名称</param>
        /// <param name="subNodeFieldName">节点名称对应的数据表字段名称</param>
        void ShowTree(DataTable dt, DataTable subdt, TreeView tv, string url, string nodeFieldName, string subNodeFieldName);








    }
}

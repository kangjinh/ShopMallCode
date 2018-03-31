using NH.Utility;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NH.Commons.Interface
{
    public interface IGenericDAO<T,Tid> where T : class
    {
        /// <summary>
        /// 为对象创建一个IQueryOver
        /// </summary>
        /// <returns>IQueryOver 对象</returns>
        IQueryOver<T, T> CreateQueryOver();
        /// <summary>
        /// 创建一个IQuery
        /// </summary>
        /// <param name="query">查询表达式(语句)</param>
        /// <returns></returns>
        IQuery CreateQuery(string query);
        /// <summary>
        /// 创建一个ISQLQuery
        /// </summary>
        /// <param name="query">查询表达式(语句)</param>
        /// <returns></returns>
        ISQLQuery CreateSQLQuery(string query);
        /// <summary>
        /// 创建一个ICriteria
        /// </summary>
        /// <returns>>ICriteria对象实例</returns>
        ICriteria CreateCriteria();
        /// <summary>
        /// 以指定的别名创建一个ICriteria
        /// </summary>
        /// <param name="alias">别名</param>
        /// <returns>ICriteria对象实例</returns>
        ICriteria CreateCriteria(string alias);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetEntity(Tid id);
        /// <summary>
        /// 根据Linq 表达式查询单个对象
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);
        /// <summary>
        /// 插入数据，使用给定的值
        /// </summary>
        /// <param name="entity">要插入的对象</param>
        /// <param name="id">要插入对象的ID值</param>
        /// <returns></returns>
        T Insert(T entity, Tid id);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">要更新的对象</param>
        /// <returns></returns>
        T Update(T entity);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        bool Update(string condition);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">要删除的对象</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        bool Delete(string condition);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        IList<T> GetList();
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="id">当前节点ID</param>
        /// <returns></returns>
        IList<T> GetList(int id);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(string condition);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        IList<T> GetList(Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="orderName">排序字段</param>
        /// <param name="orderBy">排序类型</param>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        IList<T> GetList(string orderName, NH.EnumLibrary.OrderBy orderBy, Expression<Func<T, bool>> condition);
        /// <summary>
        /// 根据Id字段值获取匹配的对象列表
        /// </summary>
        /// <param name="idList">对象ID列表</param>
        /// <returns>对象列表</returns>
        IList<T> GetList(params Tid[] idList);
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize);
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount);
        /// <summary>
        /// 获取条件查询分页数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 获取条件查询分页数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <returns></returns>
        IList<T> GetList(string orderName, int pageIndex, int pageSize);
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <returns></returns>
        IList<T> GetList(string orderName, int pageIndex, int pageSize, ref int totalRecordCount);
        /// <summary>
        /// 返回条件查询下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(string orderName, int pageIndex, int pageSize, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 返回条件查询下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        IList<T> GetList(string orderName, int pageIndex, int pageSize, ref int totalRecordCount, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 获取指定hql语句下的列表数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="queryString">hql语句</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, string queryString);
        /// <summary>
        /// 获取指定IQuery接口下的列表数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="query">IQuery接口</param>
        /// <returns></returns>
        IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, IQuery query);
        /// <summary>
        /// 返回列表中前top条数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <returns></returns>
        IList<T> GetTopList(int top);
        /// <summary>
        /// 返回列表中前top条数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="orderName">排序字段名称</param>
        /// <returns></returns>
        IList<T> GetTopList(int top, string orderName);
        /// <summary>
        /// 返回指定条件下的前top条记录(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        IList<T> GetTopList(int top, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 返回指定条件下的前top条记录(降序DESC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        IList<T> GetTopList(int top, string orderName, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 根据过滤条件查询并返回支持翻页的对象列表
        /// </summary>
        /// <param name="pagingInfo">分页信息</param>
        /// <param name="orderBy">排序字段信息</param>
        /// <param name="condition">过滤条件</param>
        /// <returns>带有翻页信息的对象列表</returns>
        IPagedList<T> GetObjectPagedList(IPagingInfo pagingInfo, OrderField orderBy, Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 按Query条件查询并返回支持分页的对象列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pagingInfo">分页信息</param>
        /// <param name="orderBy">排序字段信息</param>
        /// <returns>支持翻页的对象列表</returns>
        IPagedList<T> GetObjectPagedList(IQueryOver<T, T> query, IPagingInfo pagingInfo, OrderField orderBy);
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        bool IsHasChildrenNode(Expression<Func<T, bool>> condition = null);
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="id">当前节点ID</param>
        /// <returns></returns>
        bool IsHasChildrenNode(int id);
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        bool IsHasChildrenNode(string condition);
    }
}

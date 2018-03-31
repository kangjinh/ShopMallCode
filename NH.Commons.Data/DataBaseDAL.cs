using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NH.Commons.Interface;
using NHibernate.Metadata;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NH.Utility;

namespace NH.Commons.Data
{
    public class DataBaseDAL<T, Tid> : IGenericDAO<T, Tid> where T : class
    {
        /// <summary>
        /// 实体类ClassMetadata对象
        /// </summary>
        private IClassMetadata classMetadata;
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly ISessionFactory SessionFactory;
        /// <summary>
        /// 泛型NHibernate数据层构造函数
        /// </summary>
        public DataBaseDAL()
        {
            //this.SessionFactory = NHibernateHelper.GetSessionFactory(DBConfig.Default);
            this.SessionFactory = DBHelper.SessionFactory;
        }
        /// <summary>
        /// 泛型NHibernate数据层构造函数
        /// </summary>
        /// <param name="dbConfig">DBConfig配置枚举</param>
        public DataBaseDAL(DBConfig dbConfig)
        {
            //this.SessionFactory = NHibernateHelper.GetSessionFactory(dbConfig);
            switch (dbConfig)
            {
                case DBConfig.Default:
                    this.SessionFactory = DBHelper.SessionFactory;
                    break;
                
                default:
                    this.SessionFactory = DBHelper.SessionFactory;
                    break;
            }
        }       
        /// <summary>
        /// 获取IClassMetadata接口
        /// </summary>
        protected IClassMetadata ClassMetadata
        {
            get
            {
                if (classMetadata == null)
                {
                    classMetadata = this.SessionFactory.GetClassMetadata(typeof(T));
                }
                return classMetadata;
            }
        }
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetEntity(Tid id)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.Get<T>(id);
            }
        }
        /// <summary>
        /// 根据表达式查询单个对象
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns>对象实例</returns>
        public virtual T GetEntity(Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            if (query.RowCount() > 1)
                return query.List<T>().FirstOrDefault<T>(); //return query.List<T>().LastOrDefault<T>();
            else
                return query.SingleOrDefault();

        }
        /// <summary>
        /// 通过ICriterion条件查询并返回一个对象
        /// </summary>
        /// <param name="criterion">查询条件</param>
        /// <returns>对象实例</returns>
        public virtual T GetEntity(params ICriterion[] criterion)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ICriteria crit = Session.CreateCriteria<T>();
                foreach (ICriterion c in criterion)
                {
                    crit.Add(c);
                }
                crit.SetMaxResults(1);
                IList<T> list = crit.List<T>();
                return list.Count > 0 ? list[0] : null;
            }
        }
        /// <summary>
        /// 插入数据
        /// 注意：如果数据表主键不是自动递增类型，添加前都要检查主键值是否存在
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual T Insert(T entity)
        {
            /*
            try
            {
                this.Session.Save(entity);
                this.Session.Flush();
                return entity;
            }
            catch
            {
                return null;
            }
            */
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    Session.Save(entity);
                    transaction.Commit();
                    return entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Insert_error.txt");
                    return null;
                }
            }
        }
        /// <summary>
        /// 插入数据，使用给定的值
        /// 注意：如果数据表主键不是自动递增类型，添加前都要检查主键值是否存在
        /// </summary>
        /// <param name="entity">要插入的对象</param>
        /// <param name="id">要插入对象的ID值</param>
        /// <returns></returns>
        public virtual T Insert(T entity, Tid id)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    Session.Save(entity, id);
                    transaction.Commit();
                    return entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Insert_error.txt");
                    return null;
                }
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual T Update(T entity)
        {
            //方法一
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    Session.Update(entity);
                    transaction.Commit();
                    return entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Update_error.txt");
                    return null;
                }
            }            
        }
        public virtual bool Update(string condition)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    if (!string.IsNullOrWhiteSpace(condition))
                    {
                        Session.CreateSQLQuery(condition).ExecuteUpdate();
                        //Session.Delete(string.Format("FROM {0} WHERE {1}", this.ClassMetadata.EntityName, condition));
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Update_error.txt");
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    Session.Delete(entity);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Delete_error.txt");
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual bool Delete(string condition)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ITransaction transaction = Session.BeginTransaction();
                try
                {
                    if (!string.IsNullOrWhiteSpace(condition))
                    {
                        Session.Delete(string.Format("FROM {0} WHERE {1}", this.ClassMetadata.EntityName, condition));
                    }
                    else
                    {
                        Session.Delete(string.Format("FROM {0}", this.ClassMetadata.EntityName));
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelper.WriteLog(ex.ToString() + "\r\n" + typeof(T).FullName, "HibernateDAO_Delete_error.txt");
                    return false;
                }
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetList()
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.QueryOver<T>().List();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="id">当前节点ID</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int id)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                var hqlQuery = Session.CreateQuery("FROM " + this.ClassMetadata.EntityName + " o WHERE o.ParentID = ?").SetInt32(0, id);
                return hqlQuery.List<T>();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string condition)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                string hql = null;
                if (!string.IsNullOrWhiteSpace(condition))
                    hql = string.Format("FROM {0} WHERE {1}", this.ClassMetadata.EntityName, condition);
                else
                    hql = string.Format("FROM {0}", this.ClassMetadata.EntityName);
                var hqlQuery = Session.CreateQuery(hql);
                return hqlQuery.List<T>();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string orderName, NH.EnumLibrary.OrderBy orderBy = EnumLibrary.OrderBy.DESC, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            if (orderBy == EnumLibrary.OrderBy.DESC)
            {
                return query.RootCriteria.AddOrder(Order.Desc(orderName)).List<T>();
            }
            else
            {
                return query.RootCriteria.AddOrder(Order.Asc(orderName)).List<T>();
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        public virtual IList<T> GetList(Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            return query.List<T>();
        }
        /// <summary>
        /// 根据Id字段值获取匹配的对象列表
        /// </summary>
        /// <param name="idList">对象ID列表</param>
        /// <returns>对象列表</returns>
        public virtual IList<T> GetList(params Tid[] idList)
        {
            string idName = this.ClassMetadata.IdentifierPropertyName;
            return this.GetList(Restrictions.InG(idName, idList));
        }
        /// <summary>
        /// 通过ICriterion条件查询并返回对象列表
        /// </summary>
        /// <param name="criterion">查询条件</param>
        /// <returns>对象列表</returns>
        public virtual IList<T> GetList(params ICriterion[] criterion)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ICriteria crit = Session.CreateCriteria<T>();
                foreach (ICriterion c in criterion)
                {
                    crit.Add(c);
                }
                return crit.List<T>();
            }
        }
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                int currentIndex = 0;
                if (pageIndex > 0)
                    currentIndex = pageSize * (pageIndex - 1);
                var criteria = Session.CreateCriteria<T>().SetFirstResult(currentIndex).SetMaxResults(pageSize);
                return criteria.List<T>();
            }
        }
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                int currentIndex = 0;
                if (pageIndex > 0)
                    currentIndex = pageSize * (pageIndex - 1);
                var criteria = Session.CreateCriteria<T>();
                totalRecordCount = criteria.List<T>().Count;
                criteria.SetFirstResult(currentIndex).SetMaxResults(pageSize);
                return criteria.List<T>();
            }
        }
        /// <summary>
        /// 获取条件查询分页数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            //第一种写法
            return query.RootCriteria.SetFirstResult((pageIndex - 1) * pageSize).SetMaxResults(pageSize).List<T>();
            //第二种写法
            //return query.List<T>().Where<T>(condition.Compile()).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList<T>();
        }
        /// <summary>
        /// 获取条件查询分页数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            totalRecordCount = query.RowCount();
            int currentIndex = 0;
            if (pageIndex > 0)
                currentIndex = pageSize * (pageIndex - 1);
            return query.RootCriteria.SetFirstResult(currentIndex).SetMaxResults(pageSize).List<T>();
        }
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string orderName, int pageIndex, int pageSize)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                int currentIndex = 0;
                if (pageIndex > 0)
                    currentIndex = pageSize * (pageIndex - 1);
                var criteria = Session.CreateCriteria<T>().AddOrder(Order.Desc(orderName)).SetFirstResult(currentIndex).SetMaxResults(pageSize);
                return criteria.List<T>();
            }
        }
        /// <summary>
        /// 返回列表中指定页索引下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string orderName, int pageIndex, int pageSize, ref int totalRecordCount)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                int currentIndex = 0;
                if (pageIndex > 0)
                    currentIndex = pageSize * (pageIndex - 1);
                var criteria = Session.CreateCriteria<T>();
                totalRecordCount = criteria.List<T>().Count;
                criteria.AddOrder(Order.Desc(orderName)).SetFirstResult(currentIndex).SetMaxResults(pageSize);
                return criteria.List<T>();
            }
        }
        /// <summary>
        /// 返回条件查询下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string orderName, int pageIndex, int pageSize, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            int currentIndex = 0;
            if (pageIndex > 0)
                currentIndex = pageSize * (pageIndex - 1);
            return query.RootCriteria.AddOrder(Order.Desc(orderName)).SetFirstResult(currentIndex).SetMaxResults(pageSize).List<T>();
        }
        /// <summary>
        /// 返回条件查询下的数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual IList<T> GetList(string orderName, int pageIndex, int pageSize, ref int totalRecordCount, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            totalRecordCount = query.RowCount();
            int currentIndex = 0;
            if (pageIndex > 0)
                currentIndex = pageSize * (pageIndex - 1);
            return query.RootCriteria.AddOrder(Order.Desc(orderName)).SetFirstResult(currentIndex).SetMaxResults(pageSize).List<T>();
        }
        /// <summary>
        /// 获取指定hql语句下的列表数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="queryString">hql语句</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, string queryString)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                IQuery query = CreateQuery(queryString);
                int currentIndex = 0;
                if (pageIndex > 0)
                    currentIndex = pageSize * (pageIndex - 1);
                totalRecordCount = query.List<T>().Count;
                return query.SetFirstResult(currentIndex).SetMaxResults(pageSize).List<T>();
            }
        }
        /// <summary>
        /// 获取指定IQuery接口下的列表数据
        /// </summary>
        /// <param name="pageIndex">数据当前页索引(索引从1开始计数)</param>
        /// <param name="pageSize">数据数量</param>
        /// <param name="totalRecordCount">列表总记录数量</param>
        /// <param name="query">IQuery接口</param>
        /// <returns></returns>
        public virtual IList<T> GetList(int pageIndex, int pageSize, ref int totalRecordCount, IQuery query)
        {
            int currentIndex = 0;
            if (pageIndex > 0)
                currentIndex = pageSize * (pageIndex - 1);
            totalRecordCount = query.List<T>().Count;
            return query.SetFirstResult(currentIndex).SetMaxResults(pageSize).List<T>();
        }
        /// <summary>
        /// 返回列表中前top条数据列表(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <returns></returns>
        public virtual IList<T> GetTopList(int top)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ICriteria crit = Session.CreateCriteria(typeof(T));
                crit.SetMaxResults(top);
                return crit.List<T>();
            }
        }
        /// <summary>
        /// 返回列表中前top条数据列表(降序DESC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="orderName">排序字段名称</param>
        /// <returns></returns>
        public virtual IList<T> GetTopList(int top, string orderName)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                ICriteria crit = Session.CreateCriteria(typeof(T)).AddOrder(Order.Desc(orderName));
                crit.SetMaxResults(top);
                return crit.List<T>();
            }
        }
        /// <summary>
        /// 返回指定条件下的前top条记录(默认是升序ASC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        public virtual IList<T> GetTopList(int top, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            return query.Take(top).List<T>();
        }
        /// <summary>
        /// 返回指定条件下的前top条记录(降序DESC方式取值)
        /// </summary>
        /// <param name="top">数据数目</param>
        /// <param name="orderName">排序字段名称</param>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        public virtual IList<T> GetTopList(int top, string orderName, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            return query.RootCriteria.AddOrder(Order.Desc(orderName)).SetMaxResults(top).List<T>();
        }
        /// <summary>
        /// 为对象创建一个IQueryOver
        /// </summary>
        /// <returns>IQueryOver 对象</returns>
        public IQueryOver<T, T> CreateQueryOver()
        {
            ISession Session = SessionFactory.OpenSession();
            return Session.QueryOver<T>();
        }
        /// <summary>
        /// 创建一个IQuery
        /// </summary>
        /// <param name="query">查询表达式(语句)</param>
        /// <returns></returns>
        public IQuery CreateQuery(string query)
        {
            /*
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.CreateQuery(query);
            }
            */
            ISession Session = SessionFactory.OpenSession();
            return Session.CreateQuery(query);
        }
        /// <summary>
        /// 创建一个ISQLQuery
        /// </summary>
        /// <param name="query">查询表达式(语句)</param>
        /// <returns></returns>
        public ISQLQuery CreateSQLQuery(string query)
        {
            /*
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.CreateSQLQuery(query);
            }
            */
            ISession Session = SessionFactory.OpenSession();
            return Session.CreateSQLQuery(query);
        }
        /// <summary>
        /// 创建一个ICriteria
        /// </summary>
        /// <returns>>ICriteria对象实例</returns>
        public ICriteria CreateCriteria()
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.CreateCriteria<T>();
            }
        }
        /// <summary>
        /// 以指定的别名创建一个ICriteria
        /// </summary>
        /// <param name="alias">别名</param>
        /// <returns>ICriteria对象实例</returns>
        public ICriteria CreateCriteria(string alias)
        {
            using (ISession Session = SessionFactory.OpenSession())
            {
                return Session.CreateCriteria<T>(alias);
            }
        }
        /// <summary>
        /// 根据过滤条件查询并返回支持翻页的对象列表
        /// </summary>
        /// <param name="pagingInfo">分页信息</param>
        /// <param name="orderBy">排序字段信息</param>
        /// <param name="condition">过滤条件</param>
        /// <returns>带有翻页信息的对象列表</returns>
        public virtual IPagedList<T> GetObjectPagedList(IPagingInfo pagingInfo, OrderField orderBy, Expression<Func<T, bool>> condition = null)
        {
            var query = this.CreateQueryOver();
            if (condition != null)
            {
                query.Where(condition);
            }
            bool needPaging = pagingInfo != null && pagingInfo.IsLegal;
            if (needPaging)
            {
                if (pagingInfo.DoCount)
                {
                    pagingInfo.RecordCount = query.RowCountInt64();
                }
                query.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize);
            }

            query.RootCriteria.AddOrder(new Order(orderBy.PropertyName, orderBy.Ascending));
            IList<T> list = query.List<T>();
            return needPaging ? list.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize, (int)pagingInfo.RecordCount) : list.ToPagedList(1, list.Count);
        }
        /// <summary>
        /// 按Query条件查询并返回支持分页的对象列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pagingInfo">分页信息</param>
        /// <param name="orderBy">排序字段信息</param>
        /// <returns>支持翻页的对象列表</returns>
        public virtual IPagedList<T> GetObjectPagedList(IQueryOver<T, T> query, IPagingInfo pagingInfo, OrderField orderBy)
        {
            bool needPaging = pagingInfo != null && pagingInfo.IsLegal;
            if (needPaging)
            {
                if (pagingInfo.DoCount)
                {
                    pagingInfo.RecordCount = query.RowCountInt64();
                }
                query.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize);
            }

            query.RootCriteria.AddOrder(new Order(orderBy.PropertyName, orderBy.Ascending));
            IList<T> list = query.List<T>();
            return needPaging ? list.ToPagedList(pagingInfo.CurrentPage, pagingInfo.PageSize, (int)pagingInfo.RecordCount) : list.ToPagedList(1, list.Count);
        }
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="condition">查询表达式</param>
        /// <returns></returns>
        public bool IsHasChildrenNode(Expression<Func<T, bool>> condition = null)
        {
            bool flag = false;
            IList<T> list = GetList(condition);
            if (list.Count > 0)
                flag = true;
            return flag;
        }
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="id">当前节点ID</param>
        /// <returns></returns>
        public bool IsHasChildrenNode(int id)
        {
            bool flag = false;
            IList<T> list = GetList(id);
            if (list.Count > 0)
                flag = true;
            return flag;

        }
        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public bool IsHasChildrenNode(string condition)
        {
            bool flag = false;
            IList<T> list = GetList(condition);
            if (list.Count > 0)
                flag = true;
            return flag;
        }
    }
}

using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Commons.Data
{
    /// <summary>
    /// 数据库配置枚举
    /// </summary>
    public enum DBConfig
    {
        /// <summary>
        /// 第一个数据库
        /// </summary>
        Default = 0
    }
    public static class DBHelper
    {
        /// <summary>
        /// 默认数据库链接对象
        /// </summary>
        public static readonly ISessionFactory SessionFactory;
        /// <summary>
        /// 默认数据库链接配置文件路径
        /// </summary>
        private static readonly string DefaultConfigPath = NH.Commons.ConfigHelper.GetConfigValueByKey("NHibernateConfig_NHDB");  //HttpContext.Current.Server.MapPath("~/config/NHDB.hibernate.cfg.xml");
        /// <summary>
        /// 初始化各个数据库链接对象
        /// </summary>
        static DBHelper()
        {
            SessionFactory = GetFirstSessionFactory();           
        }
        /// <summary>
        /// 获取默认(三优平台)数据库链接对象
        /// </summary>
        /// <returns></returns>
        private static ISessionFactory GetFirstSessionFactory()
        {
            //return (new Configuration()).Configure(DefaultConfigPath).BuildSessionFactory();
            var configuration = new Configuration().Configure(DefaultConfigPath);
            return configuration.BuildSessionFactory();
        }

    }
}

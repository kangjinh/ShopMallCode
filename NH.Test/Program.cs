using NH.Commons;
using NH.Commons.Data;
using NH.Commons.Interface;
using NH.Entity.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Test
{
    class Program
    {        
        static void Main(string[] args)
        {
            //ISession Session = null;
            //ITransaction transaction = null;
            //try
            //{
            //    //Session = NHibernateHelper.DefaultSessionFactory.OpenSession();
            //    //transaction = Session.BeginTransaction();
            //    
            //    //mod.Password = "123456";
            //    //mod.Regplatform = 1;
            //    ////mod.IdCard = "1233333";
            //    //mod.Sex = true;
            //    //mod.Birthday = new DateTime(1984, 11, 8);
            //    //mod.UserType = 0;
            //    //mod.IsLocked = false;
            //    //mod.SaveTime = DateTime.Now;
            //    //Session.Save(mod);

            //    transaction.Commit();
            //    Console.WriteLine("保存成功");
            //}
            //catch(Exception ex)
            //{
            //    transaction.Rollback();
            //}
            //finally
            //{
            //    transaction.Dispose();
            //    Session.Close();
            //    Session.Dispose();
            //}

            //string value = BaiduAPI.GetAddress("city", 31.8691883, 106.7579193);
            //Console.Write(value);
            Console.ReadKey();
        }
    }
}

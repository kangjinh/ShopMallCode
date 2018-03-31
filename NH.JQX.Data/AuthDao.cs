using NH.Commons;
using NH.Commons.Data;
using NH.Entity.Model;
using NH.Entity.Web;
using NH.JQX.Interface;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Data
{
    /// <summary>
    /// 用户注册登录类
    /// </summary>
    public class AuthDao : IAuthDao
    {
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public virtual int Regieste(Entity.Web.RegisterReq entity)
        {
            ISession Session = null;
            ITransaction transaction = null;
            try
            {
                Session = DBHelper.SessionFactory.OpenSession();
                transaction = Session.BeginTransaction();
                Users mod = new Users();
                mod.UserName = entity.username;
                mod.Phone = entity.phone;
                mod.NickName = entity.username;
                mod.Password = Encrypt.MD5(entity.password);
                mod.Regplatform = 1;
                //mod.IdCard = "1233333";
                mod.Sex = true;
                mod.Birthday = DateTime.Now;
                mod.UserType = 0;
                mod.IsLocked = false;
                mod.SaveTime = DateTime.Now;
                Session.Save(mod);

                transaction.Commit();
                return mod.ID;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }
            finally
            {
                transaction.Dispose();
                Session.Close();
                Session.Dispose();
            }
        }
        #endregion

        #region 登录
        /// <summary>
        /// 用户名和密码登录
        /// </summary>
        /// <param name="username">type为0时username代表登录名，type为1时username代表手机号</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual int LoginUp(string username,string password,int type)
        {
            ISession Session = null;            
            try
            {
                string newpass = Encrypt.MD5(password); //MD5加密
                Session = DBHelper.SessionFactory.OpenSession();
                var query = Session.QueryOver<Users>();
                if (type==0)
                {
                    query.Where(c => c.UserName == username && c.Password == newpass);                    
                }
                else
                {
                    query.Where(c => c.Phone == username && c.Password == newpass);   
                }
                Users mod = null;
                if (query.RowCount()>1)
                {
                    mod = query.List().FirstOrDefault();
                }
                else
                {
                    mod = query.SingleOrDefault();
                }
                if (mod!=null && mod.ID>0)
                {
                    return mod.ID;
                }
                return 0;
            }
            catch (Exception ex)
            {                
                return 0;
            }
            finally
            {                
                Session.Close();
                Session.Dispose();
            }
        }
        #endregion

         #region 重置密码

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual int ResetPassword(ResetPasswordReq req)
        {
            ISession Session = null;
            ITransaction transaction = null;
            try
            {
                Session = DBHelper.SessionFactory.OpenSession();   
                transaction = Session.BeginTransaction();
                var query = Session.QueryOver<Users>();
                query.Where(c => c.Phone == req.phone && c.IsLocked == false);
                //query.RootCriteria.AddOrder(Order.Desc("ID")).List<AuthVerify>();
                Users mod = null;
                if (query.RowCount() > 1)
                {
                    mod = query.List().FirstOrDefault();
                }
                else
                {
                    mod = query.SingleOrDefault();
                }
                if (mod != null && mod.ID > 0)
                {
                    mod.Password = Encrypt.MD5(req.password); //MD5加密
                    Session.Update(mod);
                    transaction.Commit();
                    if (mod!=null && mod.ID >0)
                    {
                        return mod.ID;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                LogHelper.WriteLog("重置密码出错：" + ex.Message, "excep_AuthDao.txt");
                return 0;
            }
            finally
            {
                Session.Close();
                Session.Dispose();
            }
        }
        #endregion 重置密码
    }
}

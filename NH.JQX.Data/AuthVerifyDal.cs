using NH.Commons;
using NH.Commons.Data;
using NH.Entity.Model;
using NH.Entity.Web;
using NH.JQX.Interface;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.JQX.Data
{
    public class AuthVerifyDal : IAuthVerifyDal
    {
        #region 创建验证码
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual bool CreateVerifyCode(string phone, string code)
        {
            ISession Session = null;
            ITransaction transaction = null;
            try
            {
                Session = DBHelper.SessionFactory.OpenSession();
                transaction = Session.BeginTransaction();
                AuthVerify mod = new AuthVerify();
                if (Utils.IsValidMobile(phone)) //如果是手机号码
                {
                    mod.Phone = phone;
                }
                else
                {
                    mod.UserName = phone;
                }
                mod.VerifyCode = code;
                Session.Save(mod);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Dispose();
                Session.Close();
                Session.Dispose();
            }
        }
        #endregion 创建验证码

        #region 提交验证码
        /// <summary>
        /// 提交验证码
        /// </summary>
        /// <param name="req"></param>
        /// <returns>1:成功；0：验证码过期；-1，用户不存在；-2：手机号码未注册</returns>
        public virtual int SubmitCode(SubmitCodeReq req)
        {
            ISession Session = null;
            try
            {                
                Session = DBHelper.SessionFactory.OpenSession();
                var query = Session.QueryOver<AuthVerify>();
                if (Utils.IsValidMobile(req.phone)) //如果是手机号码
                {
                    query.Where(c => c.Phone == req.phone && c.VerifyCode == req.code);
                }
                else
                {
                    query.Where(c => c.UserName == req.phone && c.VerifyCode == req.code);
                    if (query.RowCount() == 0) //如果不存在用户名的验证码，则去用户表查手机
                    {
                        var query2 = Session.QueryOver<Users>();
                        query2.Where(r => r.UserName == req.phone && r.IsLocked == false);
                        var usermod = query2.SingleOrDefault();
                        if (usermod != null)
                        {
                            query.Where(c => c.Phone == usermod.Phone && c.VerifyCode == req.code);
                        }
                        else
                        {
                            return -1; //该用户在系统不存在
                        }
                    }
                }
                query.RootCriteria.AddOrder(Order.Desc("ID")).List<AuthVerify>();
                AuthVerify mod = null;
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
                    if(mod.CreateTime.AddMinutes(mod.Expired) > DateTime.Now)
                    {
                        return 1;
                    }
                    return 0; //验证码过期
                }
                return -2; //该手机号码未注册
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("用户提交验证码出错：SubmitCode," + ex.Message, "Exception_AuthVerifyDal.txt");
                return 0;
            }
            finally
            {
                Session.Close();
                Session.Dispose();
            }
        }
        #endregion

       
    }
}

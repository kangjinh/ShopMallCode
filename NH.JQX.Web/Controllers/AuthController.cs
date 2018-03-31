using NH.Commons;
using NH.Entity.Web;
using NH.JQX.App;
using NH.JQX.APP;
using NH.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.JQX.Web.Controllers
{
    public class AuthController : CONBase
    {
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        private readonly static object lockhelper = new object();
        [HttpPost]        
        public virtual ActionResult Register(RegisterReq mod)
        {
            lock (lockhelper)
            {
                if (string.IsNullOrEmpty(mod.phone))
                {
                    return ResultUtils.Error("手机号码不能为空");
                }
                else if (!Utils.IsValidMobile(mod.phone))
                {
                    return ResultUtils.Error("手机号码格式有误");
                }
                if (string.IsNullOrEmpty(mod.code))
                {
                    return ResultUtils.Error("验证码不能为空");
                }
                else if (mod.code.Trim().Length != 6)
                {
                    return ResultUtils.Error("验证码有误");
                }
                else
                {

                }
                if (string.IsNullOrEmpty(mod.username))
                {
                    mod.username = mod.phone;
                }
                if (string.IsNullOrEmpty(mod.password))
                {
                    return ResultUtils.Error("密码不能为空");
                }
                userid = NH.Service.Api.AuthService.GetInstance().Regieste(mod);
                if (userid>0)
                {
                    Entity.Model.Token token = base.GetToken();
                    return ResultUtils.Success(new
                    {
                        userId = token.UserID,
                        timeStamp = token.Timestamp,
                        platform = token.Platform,                        
                        token = token.TokenStr
                    });
                }
                return ResultUtils.Fail("注册失败");
            }
        }

        #endregion

        #region 登录

        public virtual JsonResult Login()
        {
            //if (!VerifyToken())
            //{
            //    return ResultUtils.Fail(app_senre, msg_box);
            //}
            string username = RequestHelper.GetFormString("username");
            string password = RequestHelper.GetFormString("password");
            if (Utils.StrIsNullOrEmpty(username) || Utils.StrIsNullOrEmpty(password))
            {
                return ResultUtils.Error("账号或密码不能为空");
            }
            if (Utils.IsValidMobile(username))  //判断是否是手机号码
            {
                userid = NH.Service.Api.AuthService.GetInstance().LoginUp(username, password, 1); //手机号码
            }
            else
            {
                userid = NH.Service.Api.AuthService.GetInstance().LoginUp(username, password, 0); //用户名
            }
            if (userid > 0)
            {
                Entity.Model.Token token = base.GetToken();
                return ResultUtils.Success(new
                {
                    userId = token.UserID,
                    timeStamp = token.Timestamp,
                    platform = token.Platform,
                    token = token.TokenStr
                });
            }
            return ResultUtils.Fail("登录失败");
        }
        #endregion

        #region 
        /// <summary>
        /// 请求发送验证码
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public virtual JsonResult SendCode(SendCodeReq mod)
        {
            if (string.IsNullOrEmpty(mod.phone))
            {
                return ResultUtils.Fail("手机号码不能为空");
            }
            string randownum = DataCheck.RandomNumber(4); //获取4位随机码
            bool b = VerifyService.Instance().CreateVerifyCode(mod.phone, randownum);
            if (b)
            {
                return ResultUtils.Success(new
                {
                    verifycode = randownum
                });
            }
            return  ResultUtils.Fail("请重新获取");
        }
        #endregion

        #region 验证验证码
        /// <summary>
        /// 提交验证码
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public virtual JsonResult SubmitCode(SubmitCodeReq mod)
        {
            if (string.IsNullOrEmpty(mod.phone))
            {
                return ResultUtils.Fail("手机号码或用户名不能为空");
            }
            if (string.IsNullOrEmpty(mod.code))
            {
                return ResultUtils.Fail("验证码不能为空");
            }
            int ret = NH.Service.VerifyService.Instance().SubmitCode(mod);
            if (ret==1)
            {
                return ResultUtils.Success("验证成功");
            }
            else if (ret==-1)
            {
                return ResultUtils.Fail("用户不存在");
            }
            else if (ret==-2)
            {
                return ResultUtils.Fail("手机号码未注册");
            }
            return ResultUtils.Fail("验证码过期");
        }
        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual JsonResult ResetPassword(ResetPasswordReq req)
        {
            if (string.IsNullOrEmpty(req.phone))
            {
                return ResultUtils.Fail("手机号码或用户名不能为空");
            }
            if (string.IsNullOrEmpty(req.code))
            {
                return ResultUtils.Fail("验证码不能为空");
            }
            if (string.IsNullOrEmpty(req.password))
            {
                return ResultUtils.Fail("密码不能为空");
            }
            SubmitCodeReq mod = new SubmitCodeReq()
            {
                phone = req.phone,
                code = req.code
            };
            int ret = NH.Service.VerifyService.Instance().SubmitCode(mod);
            if(ret==0)
            {
                return ResultUtils.Success("验证码过期");
            }
            else if (ret==-1)
            {
                return ResultUtils.Fail("用户不存在");
            }
            else if (ret==-2)
            {
                return ResultUtils.Fail("手机号码未注册");
            }
            userid = NH.Service.Api.AuthService.GetInstance().ResetPassword(req);
            if (userid > 0)
            {
                Entity.Model.Token token = base.GetToken();
                return ResultUtils.Success(new
                {
                    userId = token.UserID,
                    timeStamp = token.Timestamp,
                    platform = token.Platform,
                    token = token.TokenStr
                });
            }
            return ResultUtils.Fail("重置密码失败");
        }
        #endregion

        #region 获取一级类目
        /// <summary>
        /// 获取一级类目
        /// </summary>
        /// <returns></returns>
        public virtual JsonResult GetRecommendTag()
        {
            var list = NH.Service.CategoryService.Instance().GetParentCategoryList();
            if (list!=null)
            {
                return ResultUtils.Success(new
                {
                    list = list
                });
            }
            return ResultUtils.Fail("获取类目列表失败");
        }
        #endregion 获取一个类目

        #region 用户提交感兴趣的类目

        public virtual JsonResult IntersetingCategory(UserCategoryReq request)
        {
            if (!VerifyToken())
            {
                return ResultUtils.Fail(app_senre, msg_box);
            }
            if (string.IsNullOrEmpty(request.categoryids))
            {
                return ResultUtils.Fail("请选择感兴趣的类目");
            }
            string[] arr = request.categoryids.Split(',');
            var list = arr.Select(c => c.ToInt()).ToArray();
            int ret = NH.Service.CategoryService.Instance().InterstingCategory(userid, list);
            if (ret>0)
            {
                return ResultUtils.Success("提交成功");
            }
            return ResultUtils.Fail("提交失败");
        }
        #endregion
    }
}

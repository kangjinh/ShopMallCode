
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NH.JQX.App;
using NH.Entity.Web;
using NH.Entity.Web.Request;
using NH.JQX.APP;
using NH.Commons;

namespace NH.JQX.Web.Controllers
{
    public class IndexController : CONBase
    {
        //
        // GET: /Index/

        //public ActionResult Index()
        //{
        //    ViewBag.Content = "这是一个测试网站，测试成功";
        //    return View();
        //}
        /// <summary>
        /// 用户注册---测试
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        //[HttpPost]
        //public virtual ActionResult TestRegister(RegisterReq mod)
        //{
        //    if (mod.appVersion!="0.0.1")
        //    {
        //        return ResultUtils.Fail("版本号错误");
        //    }
        //    if (mod.client==0)
        //    {
        //        return ResultUtils.Fail("手机类型错误");
        //    }
        //    return ResultUtils.Success("注册成功");
        //}
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index(PositionReq req)
        {
            string city = "广州市";
            if (req.lat > 0 && req.lng >0) //有传经纬度过来
            {
                city = BaiduAPI.GetAddress("city", req.lat, req.lng);
            }
            if (VerifyToken())
            {
                
            }
            return null;
        }
    }
}

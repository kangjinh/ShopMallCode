using NH.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.Web.App
{
    /* 已经放到SanYou.API.TimingActionFilter L.J.X 2016.07.22
     * 
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TimingActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        private bool islock = false;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!islock)
            {
                GetTimer(filterContext, "action").Start();
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!islock)
            {
                GetTimer(filterContext, "action").Stop();
            }
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!islock)
            {
                var renderTimer = GetTimer(filterContext, "render");
                renderTimer.Stop();
                var actionTimer = GetTimer(filterContext, "action");

                if (!SanYou.Utility.Config.CommonConfig.IsSaveInterfaceVisitLogs)
                {
                    //写入文本文件
                    if (actionTimer.ElapsedMilliseconds >= 1 || renderTimer.ElapsedMilliseconds >= 1)
                    {
                        WriteLog("运营监控(" + filterContext.RouteData.Values["controller"] + ")",
                            String.Format("【{0}】-【{1}】,执行:{2}ms,渲染:{3}ms{4}",
                                filterContext.RouteData.Values["controller"],
                                filterContext.RouteData.Values["action"],
                                actionTimer.ElapsedMilliseconds,
                                renderTimer.ElapsedMilliseconds,
                                RequestParams(filterContext)
                            ));
                    }
                }
                else
                {
                    //接口访问日志 L.J.X 2016.06.12
                    //int vUserId = filterContext.HttpContext.Request.Form["userId"] != null ? filterContext.HttpContext.Request.Form["userId"].ToString().ToInt() : 0;
                    var kv = filterContext.HttpContext.Request.Form.AllKeys;
                    var dic = kv.Where(k => !string.IsNullOrEmpty(k)).ToDictionary(k => k, v => filterContext.HttpContext.Request.Form[v].Length > 300 ? "<超出300字符>" : filterContext.HttpContext.Request.Form[v]);
                    string ip = filterContext.HttpContext.Request.UserHostAddress;
                    string url = filterContext.HttpContext.Request.Url.ToString();
                    string sessionId = "";
                    try
                    {
                        sessionId = filterContext.HttpContext.Session.SessionID;
                    }
                    catch { }
                    long actionTime = actionTimer.ElapsedMilliseconds;
                    long renderTime = renderTimer.ElapsedMilliseconds;
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        SanYou.Service.Api.V2.CommonService.GetIntance().InterfaceVisitLogs(actionTime, renderTime, ip, url, sessionId, dic);
                    });
                }
            }
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!islock)
            {
                GetTimer(filterContext, "render").Start();
            }
            base.OnResultExecuting(filterContext);
        }

        private Stopwatch GetTimer(ControllerContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.HttpContext.Items.Contains(key))
            {
                return (Stopwatch)context.HttpContext.Items[key];
            }

            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }

        private string RequestParams(ResultExecutedContext filterContext)
        {
            if (SanYou.Utility.Config.CommonConfig.APP_TEST)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                if (filterContext.HttpContext.Request.Form != null && filterContext.HttpContext.Request.Form.Count > 0)
                {
                    int len = 0;
                    builder.Append("\r\n参数：");
                    foreach (var item in filterContext.HttpContext.Request.Form.AllKeys)
                    {
                        builder.Append("\r\n");
                        builder.Append(item);
                        builder.Append(" : ");
                        len = filterContext.HttpContext.Request.Form[item].Length;
                        if (len > 500)
                        {
                            builder.Append(filterContext.HttpContext.Request.Form[item].Substring(0, 50));
                        }
                        else
                        {
                            builder.Append(filterContext.HttpContext.Request.Form[item]);
                        }
                    }
                }
                if (filterContext.HttpContext.Request.QueryString != null && filterContext.HttpContext.Request.QueryString.Count > 0)
                {
                    if (builder.Length == 0)
                    {
                        builder.Append("\r\n参数：");
                    }
                    foreach (var item in filterContext.HttpContext.Request.QueryString.AllKeys)
                    {
                        builder.Append("\r\n");
                        builder.Append(item);
                        builder.Append(" : ");
                        builder.Append(filterContext.HttpContext.Request.QueryString[item]);
                    }
                }
                return builder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void WriteLog(string filename, string content)
        {
            try
            {
                string path = Utils.GetMapPath(string.Format("/logs/monitor/{0}/", DateTime.Now.ToString("yyyyMMdd")));
                Utils.CreateDir(path);
                filename = Utils.GetMapPath(string.Format("{0}{1}.txt", path, filename));
                System.IO.File.AppendAllText(filename, string.Format("{0}\r\n{1}\r\n----------------------------------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content));
            }
            catch
            {

            }
        }
    }
     */
}
using NH.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace NH.JQX.App
{
    public class ResultUtils
    {
        public static JsonResult Error(string message)
        {
            return ResultUtils.Message(0, message);
        }

        public static JsonResult Message(string message)
        {
            return ResultUtils.Message(1, message);
        }

        public static JsonResult Message(int status, string message)
        {
            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.Data = new { status = status, message = message };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public static JsonResult Data(object objdata)
        {
            return ResultUtils.Data(-9999, objdata);
        }

        public static JsonResult Data(int status, object objdata)
        {
            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            if (status == -9999)
            {
                json.Data = objdata;
            }
            else
            {
                json.Data = new { status = status, data = objdata };
            }
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        #region 新版开发使用

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message">失败原因</param>
        /// <returns></returns>
        public static JsonResult Fail(string message)
        {
            return ResultUtils.Fail(0, message);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="message">失败原因</param>
        /// <returns></returns>
        public static JsonResult Fail(int status, string message)
        {
            return ResultUtils.Content(status, message, new object());
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns></returns>
        public static JsonResult Success(string message)
        {
            return ResultUtils.Success(message, new object());
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="message">提示语</param>
        /// <returns></returns>
        public static JsonResult Success(int status, string message)
        {
            return ResultUtils.Content(status, message, new object());
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="objdata">数据</param>
        /// <returns></returns>
        public static JsonResult Success(object objdata)
        {
            return ResultUtils.Success(string.Empty, objdata);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示语</param>
        /// <param name="objdata">数据</param>
        /// <returns></returns>
        public static JsonResult Success(string message, object objdata)
        {
            return ResultUtils.Content(1, message, objdata);
        }
        /// <summary>
        /// 结果
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="message">提示语</param>
        /// <param name="objdata">数据</param>
        /// <returns></returns>
        public static JsonResult Content(int status, string message, object objdata)
        {
            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.Data = new { status = status, message = message, data = objdata };
            //json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        #endregion
    }
}

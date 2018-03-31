using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.JQX.APP
{
    using NH.Commons;
using NH.Entity.EnumLibrary;
    using NH.Entity.Model;

    public class CONBase : Controller
    {
        public static object lockObj = new object();

        /// <summary>
        /// 用户编号
        /// </summary>
        public int userid = 0;
        /// <summary>
        /// 状态/错误/数量/返回值等等
        /// </summary>
        public int app_senre = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg_box = string.Empty;
        
        /// <summary>
        /// 系统类型：0表示未知，1表示IOS; 2表示Andriod; 3表示windowPhone,4表示H5登录，6表示PC登录
        /// 枚举：NH.Entity.EnumLibrary.PlatformType
        /// </summary>
        public int systemtype = RequestHelper.GetInt("systemType");
        /// <summary>
        /// app版本号
        /// </summary>
        public string appVersion = RequestHelper.GetString("appVersion");
        /// <summary>
        /// 手机型号
        /// </summary>
        public string models = RequestHelper.GetString("models");
        /// <summary>
        /// 手机品牌，如:Apple
        /// </summary>
        public string brand = RequestHelper.GetString("brand");
        /// <summary>
        /// 手机系统版本，如:Android 5.0
        /// </summary>
        public string systemVersion = RequestHelper.GetString("systemVersion");
        /// <summary>
        /// 手机分辨率
        /// </summary>
        public string resolution = RequestHelper.GetString("resolution");

        /// <summary>
        /// 平台
        /// </summary>
        public Regplatform platform = (Regplatform)RequestHelper.GetInt("platform");

        /// <summary>
        /// 构造函数
        /// </summary>
        public CONBase()
        {
        }

        #region 用户Token

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="verify">是否必须验证</param>
        /// <returns></returns>
        public bool VerifyToken(bool verify = true)
        {
            return this.VerifyToken(verify, true);
        }

        /// <summary>
        /// 验证 Token
        /// </summary>
        /// <param name="verify">是否必须验证</param>
        /// <param name="isApp">是否是APP</param>
        /// <returns></returns>
        public bool VerifyToken(bool verify, bool isApp)
        {
            string tokenBase64 = RequestHelper.GetString("token");
            if (!isApp && Utils.StrIsNullOrEmpty(tokenBase64))
            {
                tokenBase64 = Utils.GetCookie("token");
            }
            Entity.Model.Token token = NH.Service.Api.TokenService.GetInstance().GetToken(tokenBase64);
            if (verify)
            {
                if (token == null || !token.IsValidSecretKey || token.UserID <= 0)
                {
                    app_senre = -1;
                    msg_box = "服务器繁忙，请重新登录";
                    return false;
                }
                else if (token.IsOverdue)
                {
                    app_senre = 999;
                    msg_box = "服务器繁忙，请重新登录";
                    return false;
                }
                app_senre = 1;
                userid = token.UserID;
                platform = token.Platform;
            }
            else
            {
                if (token != null && token.IsValidSecretKey && token.UserID > 0 && !token.IsOverdue)
                {
                    app_senre = 1;
                    userid = token.UserID;
                    platform = token.Platform;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <returns></returns>
        public Entity.Model.Token GetToken()
        {
            return NH.Service.Api.TokenService.GetInstance().GetTokenEntity(userid, platform);
        }

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="tokenBase64">Base64 Token字符串</param>
        /// <returns></returns>
        public Entity.Model.Token GetToken(string tokenBase64)
        {
            Entity.Model.Token token = NH.Service.Api.TokenService.GetInstance().GetToken(tokenBase64);
            if (token != null)
            {
                userid = token.UserID;
                platform = (Regplatform)token.Platform;
            }
            return token;
        }

        #endregion
        
        /// <summary>
        /// 根据平台获取类型
        /// </summary>
        /// <param name="platform">平台</param>
        /// <returns></returns>
        public TableRecordType GetRecordType(Regplatform platform)
        {
            //switch (platform)
            //{
            //    case Regplatform.Fm:
            //        return TableRecordType.Fm;
            //    case Regplatform.MiniCourse:
            //        return TableRecordType.MiniCourse;
            //    default:
            //        break;
            //}
            return TableRecordType.Unknown;
        }

       
       

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="time">就诊时间</param>
        /// <returns></returns>
        public static string GetOrderNo(int id, DateTime time)
        {
            return id + "_" + time.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// 获取订单编号
        /// </summary>
        /// <param name="orderno">订单号</param>
        /// <returns></returns>
        public static int GetOrderID(string orderno)
        {
            if (Utils.StrIsNullOrEmpty(orderno))
            {
                return 0;
            }
            return TypeConverter.StrToInt(orderno.Split('_')[0]);
        }

        /// <summary>
        /// 接受客户端图片参数，保存图片
        /// </summary>
        /// <param name="argname">参数名称</param>
        /// <param name="recordtype">类型</param>
        /// <param name="recordid">编号</param>
        public int SaveRecordImage(string argname, TableRecordType recordtype, int recordid)
        {
            IList<string> imgList = RequestHelper.GetArray(argname);
            return SaveRecordImage(imgList, recordtype, recordid);
        }

        /// <summary>
        /// 接受客户端图片参数，保存图片
        /// </summary>
        /// <param name="imgList">图片列表</param>
        /// <param name="recordtype">类型</param>
        /// <param name="recordid">编号</param>
        public int SaveRecordImage(IList<string> imgList, TableRecordType recordtype, int recordid)
        {
            int imgcount = 0;
            //if (imgList != null && imgList.Count > 0)
            //{
            //    System.Drawing.Image image = null;
            //    string rootpath = ConfigHelper.GetConfigValueByKey("resource_physical_path");
            //    string savedir = recordtype.ToString().ToLower();
            //    string datepath = DateTime.Now.ToString("yyyyMMdd");
            //    string path = string.Format("{0}\\images\\{1}\\{2}\\", rootpath, savedir, datepath);      //原图路径
            //    string savepath = string.Format("/images/{0}/{1}/", savedir, datepath);                   //保存数据库的路径
            //    //判断目录是否存在，不存在则创建目录
            //    Utils.CreateDir(Utils.GetMapPath(path));
            //    string filename;

            //    #region 循环保存图片

            //    NH.Commons.Interface.IGenericDAO<V2PictureLibrary, int> plDao = new NH.Commons.Data.GenericHibernateDAO<V2PictureLibrary, int>();
            //    for (int i = 0; i < imgList.Count; i++)
            //    {
            //        if (Utils.StrIsNullOrEmpty(imgList[i]))
            //        {
            //            continue;
            //        }

            //        try
            //        {
            //            image = NH.Commons.PictureHelper.ConvertBase64ToImage(imgList[i]);
            //        }
            //        catch(Exception ex)
            //        {
                        
            //            NH.Service.LogsService.GetInstance().AddApplicationLogs(userid, "保存订单图片错误", "CONBase > SaveRecordImage", ex.ToString(), Session != null ? Session.SessionID : "", LogsType.Error);
            //        }

            //        if (image != null)
            //        {
            //            filename = string.Format("{0}_{1}_{2}.jpg", recordid, i, DateTime.Now.ToString("yyMMddHHmmssfff"));
            //            bool success = PictureHelper.SaveImage(image, string.Concat(path, filename), 90L);
            //            if (success)
            //            {
            //                imgcount++;
            //                plDao.Insert(new V2PictureLibrary
            //                {
            //                    RecordID = recordid,
            //                    RecordType = (int)recordtype,
            //                    WifiPicUrl = string.Concat(savepath, filename),
            //                    GPRSPicUrl = string.Concat(savepath, filename),
            //                    ThumbUrl = string.Concat(savepath, filename),
            //                    IsLogo = imgcount == 1,
            //                    SerialNo = imgcount,
            //                    SaveTime = DateTime.Now
            //                });
            //            }
            //        }
            //    }

            //    #endregion
            //}
            return imgcount;
        }

        /// <summary>
        /// 接受客户端视频数据，保存图片
        /// </summary>
        /// <param name="videoString">视频流字符(Base64格式)</param>
        /// <param name="videoThumb">视频缩略图(Base64格式)</param>
        /// <param name="recordtype">类型</param>
        /// <param name="recordid">编号(订单ID)</param>
        public int SaveRecordVideo(string videoString, string videoThumb, TableRecordType recordtype, int recordid)
        {
            int imgcount = 0;
            if (!string.IsNullOrEmpty(videoString))
            {
                string rootpath = ConfigHelper.GetConfigValueByKey("resource_physical_path");
                string savedir = recordtype.ToString().ToLower();
                string datepath = DateTime.Now.ToString("yyyyMMdd");
                string path = string.Format("{0}\\videos\\{1}\\{2}\\", rootpath, savedir, datepath);      //路径
                string savepath = string.Format("/videos/{0}/{1}/", savedir, datepath);                   //保存数据路径
                //判断目录是否存在，不存在则创建目录
                Utils.CreateDir(Utils.GetMapPath(path));

                imgcount++;
                var videoBytes = NH.Commons.PictureHelper.ConvertBase64ToByte(videoString);
                string videoFileName = string.Format("{0}_{1}.mp4", recordid, DateTime.Now.ToString("yyMMddHHmmssfff"));

                //保存视频文件
                var fs = new System.IO.FileStream(string.Concat(path, videoFileName), System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                fs.Write(videoBytes, 0, videoBytes.Length);
                fs.Close();
                
                //视频截图
                if (!string.IsNullOrEmpty(videoThumb))
                {
                    System.Drawing.Image image = null;
                    try
                    {
                        image = NH.Commons.PictureHelper.ConvertBase64ToImage(videoThumb);
                        string imageFilename = string.Concat(path, System.IO.Path.ChangeExtension(videoFileName, ".jpg"));
                        PictureHelper.SaveImage(image, imageFilename, 90L);
                    }
                    catch { }
                }

                
            }
            return imgcount;
        }

        /// <summary>
        /// 获取距离
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public string GetDistance(double distance)
        {
            if (distance > 10000)
            {
                return ">1万km";
            }
            else
            {
                return distance.ToString("F1") + "km";
            }
        }

       

        /// <summary>
        /// 显示时间格式（用户版）
        /// </summary>
        /// <returns></returns>
        protected string GetTimeShowFormat(DateTime time, DateTime endtime, int serviceType)
        {
            //if (serviceType == (int)ServiceType.Meet)
            //{
            //    return time.ToString("yyyy-MM-dd") + "(" + NH.Commons.Utils.GetWeek(time) + ") " + time.ToString("HH:mm") + "-" + endtime.ToString("HH:mm");
            //}
            //else if (serviceType == (int)ServiceType.ImageText)
            //{
            //    DateTime start = time;
            //    DateTime end = time.AddHours(24);
            //    return time.ToString("MM-dd") + "(" + NH.Commons.Utils.GetWeek(time) + ") " + time.ToString("HH:mm")
            //           + "至" +
            //           end.ToString("MM-dd") + "(" + NH.Commons.Utils.GetWeek(end) + ") " + end.ToString("HH:mm");
            //}
            //else
            //{
                return time.ToString("yyyy-MM-dd") + "(" + NH.Commons.Utils.GetWeek(time) + ")";
            //}
        }

        /// <summary>
        /// 获取专家的个性签名
        /// </summary>
        /// <param name="sign">个性签名（一句话描述）</param>
        /// <param name="isExpert">是否是专家</param>
        /// <param name="adept">擅长</param>
        /// <returns></returns>
        protected string GetExpertSign(string sign, bool isExpert, string adept)
        {
            if (Utils.StrIsNullOrEmpty(sign))
            {
                return string.Format("擅长{0}的{1}", adept, isExpert ? "疾病" : "领域");
            }
            return sign;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            System.Text.StringBuilder error = new System.Text.StringBuilder();
            error.AppendFormat("日期：{0}\r\n引发异常的方法：{1}\r\nUrl:{2}\r\n", DateTime.Now.ToString(), ex.TargetSite, (Request.Url != null ? Request.Url.ToString() : ""));
            if (Request.Form != null && Request.Form.Count > 0)
            {
                error.Append("　参数：\r\n");
                for (int i = 0; i < Request.Form.AllKeys.Length; i++)
                {
                    error.AppendFormat("　　{0}:{1}\r\n", Request.Form.AllKeys[i], Request.Form[Request.Form.AllKeys[i]].ToString().Length > 300 ? "<长度大于300>" : Request.Form[Request.Form.AllKeys[i]].ToString());
                }
                for (int i = 0; i < Request.QueryString.AllKeys.Length; i++)
                {
                    error.AppendFormat("　　{0}:{1}\r\n", Request.QueryString.AllKeys[i], Request.QueryString[Request.QueryString.AllKeys[i]].ToString());
                }
            }
            error.AppendFormat("错误信息：{0}\r\n错误堆栈：\r\n{1}\r\n", ex.Message, ex.StackTrace);
            string dir = AppDomain.CurrentDomain.BaseDirectory + "logs\\" + DateTime.Now.ToString("yyyyMM") + "\\";

            try
            {
                //NH.Service.LogsService.GetInstance().AddApplicationLogs(userid, "接口访问错误日志", "CONBase > OnException", error.ToString(), "", LogsType.Error);                
            }
            catch { }

            base.OnException(filterContext);
        }
        /// <summary>
        /// 获取用户信息返回给客户端，客户端与其他信息组成扩展字段发送给网易云信平台
        /// </summary>
        /// <param name="tokenBase64">Token值</param>
        /// <param name="platform">平台类型: 3妈妈FM; 4育儿大师专家版; 8育儿大师用户版;</param>
        /// <param name="systemType">系统类型：0表示未知，1表示IOS; 2表示Andriod; 3表示windowPhone,4表示H5专家登录，5表示H5用户登录</param>
        /// <param name="appVersion">app版本号</param>
        /// <returns></returns>
        protected string GetExtendParam(string tokenBase64, int platform, int systemType, string appVersion)
        {
            string result = null;
            NH.Entity.Model.Token token = NH.Service.Api.TokenService.GetInstance().GetToken(tokenBase64);
            if(token != null)
            {
                if(!token.IsOverdue && token.IsValidSecretKey && token.UserID > 0)
                {
                    string[] array = { userid.ToString(), "-", platform.ToString(), "-", systemtype.ToString(), "-", appVersion };
                    result = NH.Commons.DataConvert.ConvertStringToBase64(string.Concat(array));
                    if(DataConvert.ConvertStringToByte(result).Length < 1024)
                    {
                        return result;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取用户信息返回给客户端，客户端与其他信息组成扩展字段发送给网易云信平台
        /// </summary>
        /// <returns></returns>
        protected string GetExtendParam()
        {
            string result = string.Format("{0}-{1}-{2}-{3}", userid, (int)platform, systemtype, appVersion);
            result = NH.Commons.DataConvert.ConvertStringToBase64(result);
            if (DataConvert.ConvertStringToByte(result).Length < 1024)
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

       

        /// <summary>
        /// 创建事件日志
        /// </summary>
        /// <param name="operateType"></param>
        protected void AddEventLogs(NH.Entity.EnumLibrary.LogsOperateType operateType)
        {
            //NH.Service.V2.EventLogsService.AddEventLogs(new NH.Service.V2.EventLogsRequest()
            //{
            //    AppVersion = appVersion,
            //    Brand = brand,
            //    Model = models,
            //    OperateType = (int)operateType,
            //    SystemType = systemtype,
            //    RegPlatform = (int)platform,
            //    PlatformVersion = systemVersion,
            //    RecordType = platform == Regplatform.MiniCourse ? 1 : 2,
            //    Resolution = resolution,
            //    UserID = userid
            //});
        }

        
    }
}
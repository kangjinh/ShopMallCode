using NH.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Service.Api
{
    /// <summary>
    /// Token加密服务
    /// </summary>
    public class TokenService
    {
        private static object lockObj = new object();

        #region RSA加解密token
        private static RSAEncrypts RSA
        {
            get
            {
                if (_RSA == null)
                {
                    lock (lockObj)
                    {
                        if (_RSA == null)
                        {
                            _RSA = new RSAEncrypts();
                        }
                    }
                }
                return _RSA;
            }
        } private static RSAEncrypts _RSA = null;

        /// <summary>
        /// 把token字符串加密（RSA加密）
        /// </summary>
        private static string EncrypTokenByRSA(string token)
        {
            string returnString = "";
            int length = 50; //RSA不能超过58字符
            if (token.Length < length)
            {
                returnString = Encrypt(token);
            }
            else
            {
                int startindex = 0;
                string substring = token.Substring(startindex, length);
                while (substring.Length > 0)
                {
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        returnString = returnString + "#UUUUUUU#";
                    }
                    returnString = returnString + Encrypt(substring);
                    startindex = startindex + length;
                    if (token.Length > startindex)
                    {
                        if (token.Length > startindex + length)
                        {
                            substring = token.Substring(startindex, length);
                        }
                        else
                        {
                            substring = token.Substring(startindex);
                        }
                    }
                    else
                    {
                        substring = "";
                    }
                }
            }
            return returnString;
        }

        /// <summary>
        /// 把token字符串解密（RSA解密）
        /// </summary>
        private static string DecrypTokenByRSA(string encrypToken)
        {
            string returnString = "";
            string[] arrToken = System.Text.RegularExpressions.Regex.Split(encrypToken, "#UUUUUUU#", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            foreach (var s in arrToken)
            {
                returnString = returnString + Decrypt(s);
            }
            return returnString;
        }

        /// <summary>
        /// 解密（RSA）
        /// </summary>
        /// <returns></returns>
        public static string Decrypt(string s)
        {
            return RSA.Decrypt(s);
        }
        public static string DecryptFromByte(byte[] b)
        {
            return RSA.DecryptToString(b);
        }
        /// <summary>
        /// 加密（RSA）
        /// </summary>
        /// <returns></returns>
        public static string Encrypt(string s)
        {
            return RSA.Encrypt(s);
        }
        public static byte[] EncryptToByte(string b)
        {
            return RSA.EncryptToByte(b);
        }
        #endregion RSA加解密token

        #region DES加密token
        /// <summary>
        /// 把token字符串加密（DES加密）
        /// </summary>
        private static string EncrypTokenByDES(string token)
        {
            return NH.Commons.DESEncrypts.Encode(token, ConfigHelper.GetConfigValueByKey("Token_DESEncryptKey"));
        }

        /// <summary>
        /// 把token字符串解密（DES解密）
        /// </summary>
        private static string DecrypTokenByDES(string encrypToken)
        {
            return NH.Commons.DESEncrypts.Decode(encrypToken, ConfigHelper.GetConfigValueByKey("Token_DESEncryptKey"));
        }
        #endregion DES加密token

        /// <summary>
        /// 把token字符串加密
        /// </summary>
        public static string EncrypToken(string token)
        {
            int key = ConfigHelper.GetConfigValueByKey("Token_EncryptType").ToInt();
            switch (key)
            {
                case 1:
                    return EncrypTokenByRSA(token);
                case 2:
                    return EncrypTokenByDES(token);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 把token字符串解密
        /// </summary>
        public static string DecrypToken(string encrypToken)
        {
            int key = ConfigHelper.GetConfigValueByKey("Token_EncryptType").ToInt();
            switch (key)
            {
                case 1:
                    string tokenStr = "";
                    string[] arrToken = System.Text.RegularExpressions.Regex.Split(encrypToken, "#UUUUUUU#", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    foreach (var s in arrToken)
                    {
                        tokenStr = tokenStr + DecrypTokenByRSA(s);
                    }
                    return tokenStr;
                case 2:
                    return DecrypTokenByDES(encrypToken);
                default:
                    return "";
            }
        }



        /// <summary>
        /// 获取对象
        /// </summary>
        public static TokenService GetInstance()
        {
            if (_service == null)
            {
                _service = new TokenService();
            }
            return _service;
        } private static TokenService _service = null;

        /// <summary>
        /// 获取随机字符串(16位数字+字母)
        /// </summary>
        /// <returns></returns>
        private string GetNonceStr()
        {
            string inputString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = 16;
            string result = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += inputString[rd.Next(inputString.Length)];
            }
            return result;
        }
        /// <summary>
        /// 获取随机字符串(指定长度数字+字母)
        /// </summary>
        /// <param name="length">位数</param>
        /// <returns></returns>
        private string GetNonceStr(int length)
        {
            string inputString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += inputString[rd.Next(inputString.Length)];
            }
            return result;
        }
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetNonceStr(string inputString, int length)
        {
            string result = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += inputString[rd.Next(inputString.Length)];
            }
            return result;
        }
        /// <summary>
        /// 检查指定时间戳与当前时间对比,是否过期
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        private bool GetIsOverdue(long timeStamp)
        {
            DateTime dt = DataConvert.ConvertIntToDateTime(timeStamp);
            int result = DateTime.Now.CompareTo(dt);
            if (result >= 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="token">token值</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonceStr">16位随机字符串</param>
        /// <returns></returns>
        private string GetSignature(string token, string timestamp, string nonceStr)
        {
            string[] ArrTmp = { token, timestamp, nonceStr };
            Array.Sort(ArrTmp);//字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");//对该字符串进行sha1加密
            tmpStr = tmpStr.ToLower();//对字符串中的字母部分进行小写转换，非字母字符不作处理
            return tmpStr;
        }
        /// <summary>
        /// 根据用户ID计算出Base64数据的Token
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="platform">平台（详见 NH.Entity.EnumLibrary.Regplatform 枚举）</param>
        /// <returns></returns>
        public string GetToken(int userid, NH.Entity.EnumLibrary.Regplatform platform)
        {
            NH.Entity.Model.Token token = this.GetTokenEntity(userid, platform);
            return token.TokenStr;

            //if (userid <= 0)
            //    return string.Empty;
            ////密钥(32位数字+字母组成的字符串)
            //string AppKey = ConfigHelper.GetConfigValueByKey("AppKey");
            ////从当前时间开始指定天数后(截止天数)
            //int AppTokenValidDays = TypeConverter.StrToInt(ConfigHelper.GetConfigValueByKey("AppTokenValidDays"), 7);
            ////把用户ID转换成Base64字符
            //string useridBase64 = DataConvert.ConvertStringToBase64(userid.ToString());
            //long timeStamp = DataConvert.ConvertDateTimeToInt(AppTokenValidDays);
            //int authenticate = 0;
            //if (platform == NH.Entity.EnumLibrary.Regplatform.MiniCourse)
            //{
            //    authenticate = NH.Service.V2.ExpertService.GetAuthenticateStatus(userid);
            //}
            //string[] arrayTemp = { useridBase64, timeStamp.ToString(), AppKey, ((int)platform).ToString(), authenticate.ToString() };
            //string token = string.Join("-", arrayTemp);
            //return DataConvert.ConvertStringToBase64(token);
        }
        /// <summary>
        /// 根据Token的Base64字符转换成对应的Token对象
        /// </summary>
        /// <param name="tokenBase64"></param>
        /// <returns></returns>
        public NH.Entity.Model.Token GetToken(string tokenBase64)
        {
            try
            {
                if (!Utils.StrIsNullOrEmpty(tokenBase64))
                {
                    string tokenString = GetDecrypTokenString(tokenBase64);
                    string[] arrayToken = tokenString.Split(new Char[] { '-' });
                    if (arrayToken.Length > 0)
                    {
                        string useridBase64 = Convert.ToString(arrayToken[0]);
                        long timeStamp = TypeConverter.StrToInt64(arrayToken[1].ToString(), 0);
                        string appKey = Convert.ToString(arrayToken[2]);
                        bool IsValidSecretKey = GetIsValidSecretKey(appKey);
                        int platform = 0;
                        if (arrayToken.Length > 3)
                        {
                            platform = TypeConverter.StrToInt(arrayToken[3]);
                        }
                        int authenticate = 0;
                        if (arrayToken.Length > 4)
                        {
                            authenticate = TypeConverter.StrToInt(arrayToken[4]);
                        }
                        NH.Entity.Model.Token token = new NH.Entity.Model.Token
                        {
                            UserID = TypeConverter.StrToInt(DataConvert.ConvertBase64ToString(useridBase64), 0),
                            IsOverdue = GetIsOverdue(timeStamp),
                            IsValidSecretKey = IsValidSecretKey,
                            AppKey = appKey,
                            Platform = (NH.Entity.EnumLibrary.Regplatform)platform,
                            Authenticate = authenticate
                        };
                        return token;
                    }
                }
            }
            catch
            { }
            return null;
        }
        /// <summary>
        /// 获取接收到的密钥与系统设置的key是否是一致的
        /// </summary>
        /// <param name="key">接收到客户端传来的key</param>
        /// <returns></returns>
        public bool GetIsValidSecretKey(string key)
        {
            if (ConfigHelper.GetConfigValueByKey("Token_IsEncryptToken").ToInt() == 1)
            {
                //RSA加密后不需要此处的密匙
                return true;
            }
            else
            {
                //密钥(32位数字+字母组成的字符串)
                string sysKey = ConfigHelper.GetConfigValueByKey("AppKey");
                if (key == sysKey)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 输出Token验证数据
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="platform">平台（详见 NH.Entity.EnumLibrary.Regplatform 枚举）</param>
        public void GetTokenVerify(int userid, NH.Entity.EnumLibrary.Regplatform platform)
        {
            if (userid <= 0)
            {
                RequestHelper.RenderJson(new
                {
                    status = -1,
                    message = "没有登录"
                });
            }
            else
            {
                string token = GetToken(userid, platform); //用户ID+密钥,如: 18-sblvs6hl6nrlch8a2k96ubv663e129s6
                string timestamp = DataConvert.ConvertDateTimeToInt().ToString();  //时间戳
                string noncestr = GetNonceStr();   //16位随机字符串(数字+字母)
                string signature = GetSignature(token, timestamp, noncestr); //签名
                RequestHelper.RenderJson(new
                {
                    status = 1,
                    token = token,
                    timestamp = timestamp,
                    noncestr = noncestr,
                    signature = signature,
                    message = "SUCCESS"
                });
                //输入格式如下:
                /*username
                 {
                    token = "18-截止时间-sblvs6hl6nrlch8a2k96ubv663e129s6", //转成base64
                    timestamp = 1439880190210,
                    noncestr = "sblvs6hl6nrlch8a",
                    signature = "sblvs6hl6nrlch8a2k96ubv663e129s6",
                 }
                 */
            }
        }
        /// <summary>
        /// 输出Token实体
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="platform">平台（详见 NH.Entity.EnumLibrary.Regplatform 枚举）</param>
        /// <returns></returns>
        public NH.Entity.Model.Token GetTokenEntity(int userid, NH.Entity.EnumLibrary.Regplatform platform)
        {
            NH.Entity.Model.Token token = new NH.Entity.Model.Token();
            if (userid > 0)
            {
                //密钥(32位数字+字母组成的字符串)
                string AppKey = "";
                if (ConfigHelper.GetConfigValueByKey("Token_IsEncryptToken").ToInt() != 1)
                {
                    AppKey = ConfigHelper.GetConfigValueByKey("AppKey");
                }
                //从当前时间开始指定天数后(截止天数)
                int AppTokenValidDays = TypeConverter.StrToInt(ConfigHelper.GetConfigValueByKey("AppTokenValidDays"), 7);

                token.UserID = userid;
                token.Timestamp = DataConvert.ConvertDateTimeToInt(AppTokenValidDays);
                //if (platform == NH.Entity.EnumLibrary.Regplatform.MiniCourse)
                //{
                //    token.Authenticate = NH.Service.V2.ExpertService.GetAuthenticateStatus(userid);
                //}
                token.Platform = platform;

                //把用户ID转换成Base64字符
                string useridBase64 = DataConvert.ConvertStringToBase64(token.UserID.ToString());
                string[] arrayTemp = new string[] { useridBase64, token.Timestamp.ToString(), AppKey, ((int)token.Platform).ToString() };
                string tokenstr = string.Join("-", arrayTemp);

                token.TokenStr = GetEncrypTokenString(tokenstr);

                //token.UserID = userid;
                //token.Timestamp = DataConvert.ConvertDateTimeToInt();  //时间戳
                //token.Noncestr = GetNonceStr();   //16位随机字符串(数字+字母)
                //token.Signature = GetSignature(token.TokenStr, token.Timestamp.ToString(), token.Noncestr); //签名
            }
            return token;
        }

        /// <summary>
        /// 获取加密的token字符串
        /// </summary>
        /// <returns></returns>
        private string GetEncrypTokenString(string token)
        {
            string tokenStr = "";
            //判断是否配置了加密token L.J.X 2016.05.26
            if (ConfigHelper.GetConfigValueByKey("Token_IsEncryptToken").ToInt() == 1)
            {
                tokenStr = NH.Service.Api.TokenService.EncrypToken(token);
                tokenStr = tokenStr.Replace("+", "[");
            }
            else
            {
                tokenStr = DataConvert.ConvertStringToBase64(token);
            }
            return tokenStr;
        }

        /// <summary>
        /// 获取解密的token字符串
        /// </summary>
        /// <returns></returns>
        private string GetDecrypTokenString(string token)
        {
            string tokenStr = "";
            //判断是否配置了加密token L.J.X 2016.05.26
            if (ConfigHelper.GetConfigValueByKey("Token_IsEncryptToken").ToInt() == 1)
            {
                token = token.Replace("[", "+");
                tokenStr = NH.Service.Api.TokenService.DecrypToken(token);
            }
            else
            {
                tokenStr = DataConvert.ConvertBase64ToString(token);
            }
            return tokenStr;
        }
    }
}

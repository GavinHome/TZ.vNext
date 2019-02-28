//-----------------------------------------------------------------------
// <copyright file="DingDingConfiguration.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>陈天运</author>
// <date>2016.4.25</date>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using TZ.vNext.Core.Const;

namespace TZ.vNext.Core
{
    public static class DingDingConfiguration
    {
        /// <summary>
        /// 钉钉 微应用 管理员单点登录密码
        /// </summary>
        ////private static  string SSOSecret = "-ZRMkQj0-wLtSPEPDCRZTLX_NIOyxqjyWunKojxre91wPLjCAT5jMB5J0QIEa_C4";

        /// <summary>
        /// 钉钉 微应用 随机字符串(随便写)
        /// </summary>
        private static string Noncestr = "fewgrehfdch";

        /// <summary>
        /// 钉钉的Token
        /// </summary>
        private static string _accessToken = string.Empty;

        /// <summary>
        /// 钉钉JSAPI
        /// </summary>
        private static string _jsApiTicket = string.Empty;

        /// <summary>
        /// 钉钉 最后一次获取Token的时间，过期时间为7200s
        /// </summary>
        private static DateTime LastAccessTokenTime = DateTime.MinValue;

        /// <summary>
        /// 钉钉 最后一次获取JSAPITicket的时间，过期时间为7200s
        /// </summary>
        private static DateTime LastJsApiTicketTime = DateTime.MinValue;

        private static object updateTicketLock = new object();

        private static object updateTokenLock = new object();

        /// <summary>
        /// 微应用AgentId
        /// </summary>
        public static string AgentId
        {
            get { return SystemVariableConst.Dingding_AgentId; }
        }

        /// <summary>
        /// 钉钉 微应用 公司ID
        /// </summary>
        public static string CorporationId
        {
            get { return SystemVariableConst.Dingding_CorpId; }
        }

        /// <summary>
        /// 钉钉 微应用 公司密码
        /// </summary>
        public static string CorporationSecret
        {
            get { return SystemVariableConst.Dingding_CorpSecret; }
        }

        /// <summary>
        /// 钉钉 微应用 公司密码
        /// </summary>
        public static long DingdingJsTicketExpireTime
        {
            get { return Convert.ToInt64(SystemVariableConst.Dingding_JsTicketExpireTime); }
        }

        /// <summary>
        /// 钉钉 微应用 公司密码
        /// </summary>
        public static long DingdingAccessTokenExpireTime
        {
            get { return Convert.ToInt64(SystemVariableConst.Dingding_AccessTokenExpireTime); }
        }

        /// <summary>
        /// 获取钉钉的AccessToken
        /// </summary>
        public static string AccessToken
        {
            get
            {
                if (IsAccessTokenExpired)
                {
                    ////防止重复生成token
                    lock (updateTokenLock)
                    {
                        if (IsAccessTokenExpired)
                        {
                            RefreshDingdingAccessToken();
                        }
                    }
                }

                return _accessToken;
            }
        }

        /// <summary>
        /// 获取钉钉的JS API 
        /// </summary>
        private static string JsApiTicket
        {
            get
            {
                if (IsJsApiTicketExpired)
                {
                    ////防止重复生成Ticket
                    lock (updateTicketLock)
                    {
                        if (IsJsApiTicketExpired)
                        {
                            RefreshDingdingJsApiTicket();
                        }
                    }
                }

                return _jsApiTicket;
            }
        }

        /// <summary>
        /// 判断钉钉的Token是否过期
        /// </summary>
        private static bool IsAccessTokenExpired
        {
            get
            {
                return LastAccessTokenTime.AddSeconds(DingdingAccessTokenExpireTime) < DateTime.Now;
            }
        }

        /// <summary>
        /// 判断钉钉的JsApiTicket是否过期
        /// </summary>
        private static bool IsJsApiTicketExpired
        {
            get { return LastJsApiTicketTime.AddSeconds(DingdingJsTicketExpireTime) < DateTime.Now; }
        }

        /// <summary>
        /// 获取DingDing授权
        /// </summary>
        /// <param name="url">当前页面的URL （不带 #）</param>
        /// <returns>钉钉的授权配置信息</returns>
        public static dynamic GetDingdingConfiguration(string url)
        {
            var timeStamp = GetTimeStamp();

            //第三步 sha1加密
            var dataToSign = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", JsApiTicket, Noncestr, timeStamp, url);

            var signature = Sha1Caculate(dataToSign).ToLower();
            return new { corpId = CorporationId, timeStamp, nonceStr = Noncestr, signature, JsApiTicket };
        }

        /// <summary>
        /// 发起GET请求，并将结果解析成指定的类型
        /// </summary>
        /// <typeparam name="T">结果解析成的类型</typeparam>
        /// <param name="url">要请求的URL</param>
        /// <returns>解析后的结果（T）</returns>
        public static T MakeGetRequest<T>(string url)
        {
            var webRequest = WebRequest.Create(new Uri(url));

            webRequest.ContentType = "application/json; charset=utf-8";

            using (var streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// 发起POST请求，并将结果解析成指定的类型
        /// </summary>
        /// <typeparam name="T">结果解析成的类型</typeparam>
        /// <param name="url">要请求的URL</param>
        /// <param name="postData">需要发送的内容</param>
        /// <returns>解析后的结果（T）</returns>
        public static T MakePostRequest<T>(string url, string postData)
        {
            var webRequest = WebRequest.Create(new Uri(url));

            webRequest.Method = WebRequestMethods.Http.Post;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.ContentLength = byteArray.Length;

            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (var streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// 获取钉钉的AccessToken
        /// </summary>
        /// <returns>钉钉的AccessToken</returns>
        private static string GetDingdingAccessToken()
        {
            var getTokenUrl = string.Format(
                "https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}",
                CorporationId,
                CorporationSecret); // 构造URL

            var tokenResult = MakeGetRequest<Dictionary<string, string>>(getTokenUrl);

            var accessToken = tokenResult["access_token"];

            return accessToken;
        }

        /// <summary>
        /// 获取钉钉的JSAPI Ticket
        /// </summary>
        /// <param name="accessToken">获取到的AccessToken</param>
        /// <returns>钉钉 JSAPI Ticket</returns>
        private static string GetDingdingJsApiTicket(string accessToken)
        {
            var getTicketUrl = string.Format(
                "https://oapi.dingtalk.com/get_jsapi_ticket?access_token={0}",
                accessToken);

            var ticketResult = MakeGetRequest<Dictionary<string, string>>(getTicketUrl);

            return ticketResult["ticket"];
        }

        private static void RefreshDingdingAccessToken()
        {
            _accessToken = GetDingdingAccessToken();
            LastAccessTokenTime = DateTime.Now;
        }

        /// <summary>
        /// 计算给定内容的Sha1值
        /// </summary>
        /// <param name="text">给定的内容</param>
        /// <returns>Sha1值</returns>
        private static string Sha1Caculate(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text); // 注意：必须使用UTF8编码( Unicode 和 UTF8 并不一样)
            var sha1 = new SHA1CryptoServiceProvider();
            var resultBytes = sha1.ComputeHash(bytes);
            var result = BitConverter.ToString(resultBytes).Replace("-", string.Empty);
            return result;
        }

        private static void RefreshDingdingJsApiTicket()
        {
            _jsApiTicket = GetDingdingJsApiTicket(AccessToken);
            LastJsApiTicketTime = DateTime.Now;
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns>时间戳</returns>  
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
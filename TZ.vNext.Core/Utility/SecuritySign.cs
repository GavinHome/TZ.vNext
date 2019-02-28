//  -----------------------------------------------------------------------
//  <copyright file="SecuritySign.cs" company="TZEPM">
//      Copyright  TZEPM. All rights reserved.
//   </copyright>
//  <author>yangxiaomin</author>
//  <date>2018-09-27</date>
//  -----------------------------------------------------------------------

using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Xml;
using RSACryptoServiceProviderExtensions;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Core
{
    public static class SecuritySign
    {
        private const int MillisecondsOfSeconds = 1000;
        private static readonly object LockedOject = new object();
        private static readonly object LockedFunction = new object();
        private static readonly RSACryptoServiceProvider RsaCryptoServiceProvider;
        private static string _input_charset = "utf-8";
        private static string _sign_type = "SHA1";

        static SecuritySign()
        {
            if (RsaCryptoServiceProvider == null)
            {
                ////单实例对象
                lock (LockedOject)
                {
                    if (RsaCryptoServiceProvider == null)
                    {
                        RsaCryptoServiceProvider = new RSACryptoServiceProvider();
                    }
                }
            }
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        public static Dictionary<string, string> Sign(SortedDictionary<string, string> sParaTemp)
        {
            ////待签名请求参数数组
            Dictionary<string, string> sPara;

            ////过滤签名参数数组
            sPara = FilterPara(sParaTemp);

            ////待签名字符串
            var paramstr = CreateLinkString(sPara);

            ////签名结果
            var sign = SignData(Encoding.GetEncoding(_input_charset).GetBytes(paramstr));

            ////签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", sign);
            sPara.Add("sign_type", _sign_type);

            return sPara;
        }

        /// <summary>
        /// 对字符串进行签名
        /// </summary>
        /// <param name="content">签名内容</param>
        /// <returns>签名结果</returns>
        public static string Sign(string content)
        {
            var sign = SignData(Encoding.GetEncoding(_input_charset).GetBytes(content));
            return sign;
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="sParaTemp">原始数组</param>
        /// <param name="sign">签名信息</param>
        /// <returns>是否成功</returns>
        public static bool Verify(SortedDictionary<string, string> sParaTemp, string sign)
        {
            ////待验签参数数组
            Dictionary<string, string> sPara;

            ////过滤签名参数数组：过滤空值, sign与sign_type参数
            sPara = FilterPara(sParaTemp);

            //获取待签名字符串
            string preSignStr = CreateLinkString(sPara);

            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != string.Empty)
            {
                isSgin = VerifyData(Encoding.GetEncoding(_input_charset).GetBytes(preSignStr), sign);
            }

            return isSgin;
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="sign">签名</param>
        /// <returns>是否成功</returns>
        public static bool Verify(string content, string sign)
        {
            //获得验签结果
            bool isSgin = false;
            if (sign != null && sign != string.Empty)
            {
                isSgin = VerifyData(Encoding.GetEncoding(_input_charset).GetBytes(content), sign);
            }

            return isSgin;
        }

        public static bool VerifyWithTimeStamp(string content, string sign, long timeStamp, long expireTime = 180)
        {
            ////if (!IsExpired(Convert.ToInt64(timeStamp)))
            ////{
            ////    if (Verify(content, sign))
            ////    {
            ////        return true;
            ////    }
            ////}

            ////return false;

            return !IsExpired(Convert.ToInt64(timeStamp)) && Verify(content, sign);
        }

        public static bool VerifyWithTimeStamp(SortedDictionary<string, string> sParaTemp, string sign, long timeStamp, long expireTime = 180)
        {
            GuardUtils.NotNull(sParaTemp, nameof(sParaTemp));
            ////if (!IsExpired(timeStamp))
            ////{
            ////    if (Verify(sParaTemp, sParaTemp["Sign"]))
            ////    {
            ////        return true;
            ////    }
            ////}

            ////return false;

            return !IsExpired(timeStamp) && Verify(sParaTemp, sParaTemp["Sign"]);
        }

        /// <summary>
        /// 验证是否过期
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="expireTime">过期时间（秒，默认3分钟，即180秒）</param>
        /// <returns>是否过期</returns>
        private static bool IsExpired(long timeStamp, long expireTime = 180)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var currnet = Convert.ToInt64(ts.TotalMilliseconds);
            return (currnet - timeStamp) > expireTime * MillisecondsOfSeconds;
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="contentData">要签名的数据</param>
        /// <returns>签名信息</returns>
        private static string SignData(byte[] contentData)
        {
            var basePath = AppDomain.CurrentDomain.RelativeSearchPath;
            var privateKeyPath = Path.Combine(basePath, "PrivateKey.pr");
            ////验签锁：多用户验签会报错
            lock (LockedFunction)
            {
                var privatekey = ReadKeyFile(privateKeyPath);
                RsaCryptoServiceProvider.FromXmlString(privatekey);
                byte[] AOutput = RsaCryptoServiceProvider.SignData(contentData, _sign_type);

                return Convert.ToBase64String(AOutput);
            }
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="contentData">原始数据</param>
        /// <param name="sign">签名信息</param>
        /// <returns>是否成功</returns>
        private static bool VerifyData(byte[] contentData, string sign)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var pulicKeyPath = Path.Combine(basePath, "PublicKey.pb");
            ////var privateKeyPath = Path.Combine(basePath, "PrivateKey.pr");
            ////验签锁：多用户验签会报错
            lock (LockedFunction)
            {
                var pulicKey = ReadKeyFile(pulicKeyPath);
                ////RsaCryptoServiceProvider.FromXmlString(pulicKey);
                ////https://stackoverflow.com/questions/46415078/net-core-2-0-rsa-platformnotsupportedexception
                RsaCryptoServiceProvider.FromXmlStringExtend(pulicKey);
                bool isVerify = RsaCryptoServiceProvider.VerifyData(contentData, _sign_type, Convert.FromBase64String(sign));
                return isVerify;
            }
        }

        /// <summary>
        /// 读取密钥文件
        /// </summary>
        /// <param name="keyPath">密钥文件的存储路径</param>
        /// <returns>返回密钥内容</returns>
        private static string ReadKeyFile(string keyPath)
        {
            var streamReader = new StreamReader(keyPath);
            var keyValue = streamReader.ReadToEnd();
            streamReader.Close();

            return keyValue;
        }

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        private static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && temp.Value != string.Empty && temp.Value != null)
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 按照格式拼接参数
        /// </summary>
        /// <param name="dicArray">参数</param>
        /// <returns>带签名字符串</returns>
        private static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();

            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }
    }
}
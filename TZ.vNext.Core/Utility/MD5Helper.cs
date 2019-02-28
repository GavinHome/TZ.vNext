//-----------------------------------------------------------------------------------
// <copyright file="MD5Helper.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Text;

namespace TZ.vNext.Core.Utility
{
    public static class MD5Helper
    {
        public static string MD532ToLower(string value)
        {
            ////return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToLower();
            return HashPassword(value).ToLower();
        }

        public static string MD532ToUpper(string value)
        {
            ////return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5");
            return HashPassword(value);
        }

        public static string MD5UserPassword(string userName, string password)
        {
            return MD5Helper.MD532ToUpper(string.Format("{0}-{1}", userName, password));
        }

        private static string HashPassword(string inputValue)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                var strResult = BitConverter.ToString(result);

                return strResult.Replace("-", string.Empty);
            }
        }
    }
}
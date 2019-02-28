//-----------------------------------------------------------------------
// <copyright file="WebConfig.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
// <date>10/18/2017 11:27:00 PM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TZ.vNext.Core
{
    public static class WebConfig
    {
        ////public static ExcelUtility Excel => new ExcelUtility();
        public static bool EnableCache => GetSetting<bool>(nameof(EnableCache));
        public static string FileStorage => GetSetting<string>(nameof(FileStorage));
        public static string AttachementStorageDirectory => GetSetting<string>(nameof(AttachementStorageDirectory));

        /// <summary>
        /// 数据库上下文的委托
        /// </summary>
        ////public static Func<DbContext> DbContext { get; set; }
        public static DbContext DbContext { get; set; }

        /// <summary>
        /// 用于记录通用的自定义查询使用的方法委托
        /// </summary>
        public static Func<IQueryable, IQueryable> CustomQuery { get; set; }

        /// <summary>
        /// 用于记录通用的附件业务数据模型实体类型
        /// </summary>
        public static Type AttachmentEntitySetType { get; set; }

        /// <summary>
        /// 从Session中获取对应附件
        /// </summary>
        public static Func<string, object> GetAttachmentFromSession { get; set; }

        /// <summary>
        /// 更新客户端缓存的委托
        /// </summary>
        public static Action<string> UpdateLocalCache { get; set; }

        /// <summary>
        /// 校验 functionid 的委托
        /// </summary>
        public static Func<Guid, bool> AuthorizeFunction { get; set; }

        /// <summary>
        /// Generate a new <see cref="Guid"/> using the comb algorithm.
        /// </summary>
        /// <returns>Guid instance</returns>
        public static Guid NewGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// get AppSettings from system configuration
        /// </summary>
        /// <typeparam name="T">AppSettings configuration node data type</typeparam>
        /// <param name="name">AppSettings configuration node name</param>
        /// <returns>AppSettings configuration result</returns>
        public static T GetSetting<T>(string name)
        {
            try
            {
                var setting = name;
                return (T)Convert.ChangeType(setting, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="Attachment.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Entity;

namespace TZ.vNext.Model
{
    [Table("New_Attachment")]
    public class Attachment : EntitySet
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}

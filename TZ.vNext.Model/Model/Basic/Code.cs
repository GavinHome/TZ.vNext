//-----------------------------------------------------------------------------------
// <copyright file="Code.cs" company="TZ.vNext">
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
    /// <summary>
    /// Code
    /// </summary>
    [Table("T_Code")]
    public class Code : EntitySet
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// CodeId
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// Code Type Id
        /// </summary>
        public int CodeTypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}

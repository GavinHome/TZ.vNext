//-----------------------------------------------------------------------------------
// <copyright file="Function.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Model
{
    [Table("T_Function")]
    public class Function : EntitySet
    {
        /// <summary>
        ///  名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  状态
        /// </summary>
        public DataStatusEnum Status { get; set; }

        /// <summary>
        ///  父ID
        /// </summary>
        public Guid? Parent { get; set; }

        /// <summary>
        ///  是否为数据
        /// </summary>
        public bool IsDataList { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="DataStatusEnum.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
// <date>10/18/2017 11:27:00 PM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Core.Enum
{
    public enum DataStatusEnum : byte
    {
        /// <summary>
        /// 有效的
        /// </summary>
        [Description("已启用")]
        [Display(Name = "已启用")]
        Valid = 0,

        /// <summary>
        /// 无效的
        /// </summary>
        [Description("已禁用")]
        [Display(Name = "已禁用")]
        Invalid = 1,

        /// <summary>
        /// 删除的
        /// </summary>
        [Description("已删除")]
        [Display(Name = "已删除")]
        Deleted = 2,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        [Display(Name = "已作废")]
        Nullify = 3
    }
}
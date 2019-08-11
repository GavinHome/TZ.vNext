//-----------------------------------------------------------------------
// <copyright file="SalarySummaryFieldsEnum.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019-01-22 10:35:54</date>
//-----------------------------------------------------------------------
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Core.Enum
{
    public enum SalarySummaryFieldsEnum
    {
        /// <summary>
        /// 按专业板块
        /// </summary>
        [Display(Name = "专业板块")]
        [Description("专业板块")]
        Plate = 0,

        /// <summary>
        /// 按公司
        /// </summary>
        [Display(Name = "公司")]
        [Description("公司")]
        Company = 1,

        /// <summary>
        /// 按机构
        /// </summary>
        [Display(Name = "机构")]
        [Description("机构")]
        Organization = 2,

        /// <summary>
        /// 按部门
        /// </summary>
        [Display(Name = "部门")]
        [Description("部门")]
        Deparment = 3,

        /// <summary>
        /// 员工编号
        /// </summary>
        [Display(Name = "员工编号")]
        [Description("员工编号")]
        CODE = 4,
    }
}

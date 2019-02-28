//-----------------------------------------------------------------------------------
// <copyright file="FormType.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 15:18:45</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TZ.vNext.Model.Enum
{
    /// <summary>
    /// FormType
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// 公式计算项
        /// </summary>
        [Display(Name = "公式计算项")]
        [Description("公式计算项")]
        Formula = 0,

        /// <summary>
        /// 导入项
        /// </summary>
        [Display(Name = "导入项")]
        [Description("导入项")]
        Import = 1,

        /// <summary>
        /// 固定项
        /// </summary>
        [Display(Name = "固定项")]
        [Description("固定项")]
        Fixed = 2
    }
}

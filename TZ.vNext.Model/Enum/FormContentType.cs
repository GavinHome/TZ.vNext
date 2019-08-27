//  -----------------------------------------------------------------------
//  <copyright file="FormContentType.cs" company="TZ.vNext">
//      Copyright  TZ.vNext. All rights reserved.
//   </copyright>
//  <author>lixiaojun</author>
//  <date>2013-09-26</date>
//  -----------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TZ.vNext.Core.Attributes;

namespace TZ.vNext.Model.Enum
{
    [DataSource("属性类型", "/api/SuperForm/GridQueryEnumType")]
    public enum FormContentType
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Display(Name = "文本")]
        [Description("string")]
        Text = 0,

        /// <summary>
        /// 数值
        /// </summary>
        [Display(Name = "数值")]
        [Description("number")]
        Number = 1,
    }
}

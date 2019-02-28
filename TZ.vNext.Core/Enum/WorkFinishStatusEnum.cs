//-----------------------------------------------------------------------
// <copyright file="WorkFinishStatusEnum.cs" company="TZEPM">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>Yangxiaomin</author>
// <date>2018-06-20 10:35:54</date>
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace TZ.vNext.Core.Enum
{
    public enum WorkFinishStatusEnum
    {
        /// <summary>
        /// 处理中
        /// </summary>
        [Description("处理中")]
        Processing = 0,

        /// <summary>
        /// 处理失败
        /// </summary>
        [Description("处理失败")]
        Error = 1,

        /// <summary>
        /// 处理成功
        /// </summary>
        [Description("处理成功")]
        Success = 2,

        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        NotFound = 404
    }
}

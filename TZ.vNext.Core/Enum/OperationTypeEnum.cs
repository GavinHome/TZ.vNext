//-----------------------------------------------------------------------
// <copyright file="OperationTypeEnum.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>10/31/2017 11:40:42 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Core.Enum
{
    [Flags]
    public enum OperationTypeEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Display(Name = "新增")]
        Insert = 0x01,

        /// <summary>
        /// 编辑(物理修改)
        /// </summary>
        [Display(Name = "编辑")]
        Update = 0x02,

        /// <summary>
        /// 删除(物理删除)
        /// </summary>
        [Display(Name = "删除")]
        Delete = 0x04,

        /// <summary>
        /// 逻辑编辑(即不修改原始数据, 将新数据独立保存, 并修改原始数据的主键为新值, 数据状态为禁用状态)
        /// 这样子即保留了原始数据, 又不破坏引用关系
        /// </summary>
        [Display(Name = "编辑")]
        LogicUpdate = 0x08,

        /// <summary>
        /// 逻辑删除(即不删除物理数据, 只修改数据状态为禁用)
        /// 这样保留了被删除的数据, 可恢复
        /// </summary>
        [Display(Name = "删除")]
        LogicDelete = 0x10,
    }
}
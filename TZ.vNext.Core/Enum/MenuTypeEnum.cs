//-----------------------------------------------------------------------------------
// <copyright file="MenuTypeEnum.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.ComponentModel;

namespace TZ.vNext.Core.Enum
{
    public enum MenuTypeEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Create,

        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        Detail,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")] 
        Audit,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")] 
        Enable,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")] 
        Disable,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Termination,

        /// <summary>
        /// 报表生成
        /// </summary>
        [Description("报表生成")]
        ReportGenerate,

        /// <summary>
        /// 报表详细
        /// </summary>
        [Description("报表详细")]
        ReportView
    }
}

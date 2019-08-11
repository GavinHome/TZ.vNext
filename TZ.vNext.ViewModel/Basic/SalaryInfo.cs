//-----------------------------------------------------------------------------------
// <copyright file="SalaryInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 14:28:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.ViewModel
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class SalaryInfo : BaseInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 唯一标识（公式）
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 数据表单类型：文本信息0，固定计算1，公式计算2，导入数据3
        /// </summary>
        public FormType FormType { get; set; }

        /// <summary>
        /// 数据表单类型名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 薪酬类型标识：基本工资、基本补贴、导入数据。。。。Code
        /// </summary>
        public SalaryType? SalaryType { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public FormContentType FormContent { get; set; }

        /// <summary>
        /// 是否必须包含
        /// </summary>
        public bool IsIncluded { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DataStatusEnum DataStatus { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}
//-----------------------------------------------------------------------------------
// <copyright file="Salary.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 14:28:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Entity;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.Model
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    [Table("Basic_Salary")]
    public class Salary : EntitySetWithCreateAndUpdate
    {
        public Salary() 
        {
            this.IsIncluded = true;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 唯一标识,内置的薪酬项请自定义唯一标识（拼音），导入项目随机生成，或者以Name为唯一标识
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 数据表单类型：公式计算：模板编辑项0；导入数据：手动导入1；固化项：固定取数（基础项)2；
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
        /// 显示顺序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}
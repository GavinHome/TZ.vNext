//-----------------------------------------------------------------------------------
// <copyright file="SalaryType.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 15:18:45</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Model.Enum
{
    public enum SalaryType
    {
        /// <summary>
        /// 公式计算
        /// </summary>
        [Display(Name = "公式计算")]
        [Description("公式计算")]
        Formula = 0,

        /// <summary>
        /// 导入数据
        /// </summary>
        [Display(Name = "导入数据")]
        [Description("导入数据")]
        Import = 1,

        /// <summary>
        /// 工号
        /// </summary>
        [Display(Name = "工号")]
        [Description("工号")]
        Code = 2,

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Description("姓名")]
        Name = 3,

        /// <summary>
        /// 板块
        /// </summary>
        [Display(Name = "板块")]
        [Description("板块")]
        Plate = 4,

        /// <summary>
        /// 所属公司
        /// </summary>
        [Display(Name = "所属公司")]
        [Description("所属公司")]
        Company = 5,

        /// <summary>
        /// 所属机构
        /// </summary>
        [Display(Name = "所属机构")]
        [Description("所属机构")]
        Organization = 6,

        /// <summary>
        /// 所在部门
        /// </summary>
        [Display(Name = "所在部门")]
        [Description("所在部门")]
        Deparment = 7,

        /// <summary>
        /// 薪档
        /// </summary>
        [Display(Name = "薪档")]
        [Description("薪档")]
        SalaryCode = 8,

        /// <summary>
        /// 基数
        /// </summary>
        [Display(Name = "基数")]
        [Description("基数")]
        Base = 9,

        /// <summary>
        /// 系数
        /// </summary>
        [Display(Name = "系数")]
        [Description("系数")]
        Coef = 10,

        /// <summary>
        /// 月薪小计
        /// </summary>
        [Display(Name = "月薪小计")]
        [Description("月薪小计")]
        MonthTotal = 11,

        /// <summary>
        /// 基本补助
        /// </summary>
        [Display(Name = "基本补助")]
        [Description("基本补助")]
        Subsidy = 12,

        /// <summary>
        /// 工时积分工资
        /// </summary>
        [Display(Name = "工时积分工资")]
        [Description("工时积分工资")]
        PointSalary = 13,

        /// <summary>
        /// 高生活成本
        /// </summary>
        [Display(Name = "高生活成本")]
        [Description("高生活成本")]
        Cost = 14,

        /// <summary>
        /// 社保扣款
        /// </summary>
        [Display(Name = "社保扣款")]
        [Description("社保扣款")]
        SocialSecurity = 15,

        /// <summary>
        /// 住房公积金扣款
        /// </summary>
        [Display(Name = "住房公积金扣款")]
        [Description("住房公积金扣款")]
        Reserve = 16,

        /// <summary>
        /// 个人所得税
        /// </summary>
        [Display(Name = "个人所得税")]
        [Description("个人所得税")]
        IncomeTax = 17,

        /// <summary>
        /// 五险一金公司缴纳
        /// </summary>
        [Display(Name = "五险一金公司缴纳")]
        [Description("五险一金公司缴纳")]
        CompanyInsurance = 18,

        /// <summary>
        /// 五险一金个人缴纳
        /// </summary>
        [Display(Name = "五险一金个人缴纳")]
        [Description("五险一金个人缴纳")]
        PersonalInsurance = 19,

        /// <summary>
        /// 五险一金公司缴纳总计
        /// </summary>
        [Display(Name = "五险一金公司缴纳总计")]
        [Description("五险一金公司缴纳总计")]
        CompanyTotal = 20,

        /// <summary>
        /// 五险一金个人缴纳总计
        /// </summary>
        [Display(Name = "五险一金个人缴纳总计")]
        [Description("五险一金个人缴纳总计")]
        PersonalTotal = 21,

        /// <summary>
        /// 其他固定项：无法枚举完的其他固定项
        /// </summary>
        [Display(Name = "Other")]
        [Description("Other")]
        Other
    }
}

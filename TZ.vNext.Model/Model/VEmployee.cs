//-----------------------------------------------------------------------------------
// <copyright file="VEmployee.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Entity;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.Model
{
    /// <summary>
    /// VEmployee
    /// </summary>
    [Table("TZSL_View_Employee")]
    public class VEmployee : EntitySet
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public Guid? OrganizationId { get; set; }
    }
}

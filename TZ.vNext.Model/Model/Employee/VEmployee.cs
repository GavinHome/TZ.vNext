//-----------------------------------------------------------------------------------
// <copyright file="VEmployee.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Core.Entity;

namespace TZ.vNext.Model
{
    /// <summary>
    /// VEmployee
    /// </summary>
    [Table("TZSL_View_Employee")]	
    [DataSource("员工数据", "/api/SuperForm/GridQueryEmployees")]
    public class VEmployee : EntitySet
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Description("账号")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        [Description("组织机构")]
        public Guid? OrganizationId { get; set; }
    }
}

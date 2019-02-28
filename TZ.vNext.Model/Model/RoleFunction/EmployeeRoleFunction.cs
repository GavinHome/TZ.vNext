//-----------------------------------------------------------------------------------
// <copyright file="EmployeeRoleFunction.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Model
{
    [Table("TZSL_View_EmployeeRoleFunction")]
    public class EmployeeRoleFunction : EntitySet
    {
        /// <summary>
        ///  账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid FunctionId { get; set; }
    }
}

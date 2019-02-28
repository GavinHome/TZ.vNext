//-----------------------------------------------------------------------------------
// <copyright file="PermissionRequirement.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

namespace TZ.vNext.Web.PermissionExtensions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string[] permissions)
        {
            Permissions = permissions;
        }

        public string[] Permissions { get; set; }
    }
}

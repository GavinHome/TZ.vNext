//-----------------------------------------------------------------------------------
// <copyright file="AuthorizePermissionAttribute.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Mvc;

namespace TZ.vNext.Web.PermissionExtensions
{
    public class AuthorizePermissionAttribute : TypeFilterAttribute
    {
        public AuthorizePermissionAttribute(params string[] permissions)
            : base(typeof(PermissionFilterAttribute))
        {
            Arguments = new[] { new PermissionRequirement(permissions) };
            Order = int.MinValue;
        }
    }
}

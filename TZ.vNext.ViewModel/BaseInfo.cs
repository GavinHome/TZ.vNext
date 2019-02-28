//-----------------------------------------------------------------------------------
// <copyright file="BaseInfo.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TZ.vNext.ViewModel
{
    public class BaseInfo
    {
        public BaseInfo()
        {
            Menus = new List<string>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public ClaimsPrincipal User { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public IList<string> Menus { get; set; }
    }
}

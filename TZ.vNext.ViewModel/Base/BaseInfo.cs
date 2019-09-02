//-----------------------------------------------------------------------------------
// <copyright file="BaseInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
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
    public class BaseInfo : IViewModelOfType<Guid>
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

        object IViewModel.Id => Id;
    }
}

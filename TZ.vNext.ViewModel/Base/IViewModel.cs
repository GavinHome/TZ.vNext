//-----------------------------------------------------------------------
// <copyright file="IViewModel.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2017/7/22 12:52:35</date>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;

namespace TZ.vNext.ViewModel
{
    public interface IViewModel
    {
        object Id { get; }

        /// <summary>
        /// 身份
        /// </summary>
        ClaimsPrincipal User { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        IList<string> Menus { get; set; }
    }
}

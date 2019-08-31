//-----------------------------------------------------------------------------------
// <copyright file="MongoBaseInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 10:47:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TZ.vNext.ViewModel
{
    public class MongoBaseInfo
    {
        public MongoBaseInfo()
        {
            Menus = new List<string>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; set; }

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

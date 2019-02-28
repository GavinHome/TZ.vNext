//-----------------------------------------------------------------------------------
// <copyright file="UserAuth.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TZ.vNext.Model
{
    public class UserAuth
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public IList<Guid> Functions { get; set; }
    }
}

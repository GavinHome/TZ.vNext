//-----------------------------------------------------------------------------------
// <copyright file="QueryParams.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2018/12/25 15:48:09</date>
// <description></description>
//-----------------------------------------------------------------------------------
using System;

namespace TZ.vNext.ViewModel
{
    public class QueryParams
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Guid? PersonalType { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public Guid? OrgId { get; set; }
    }
}

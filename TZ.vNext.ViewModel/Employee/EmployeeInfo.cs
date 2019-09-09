//-----------------------------------------------------------------------------------
// <copyright file="EmployeeInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/09 08:28:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.ViewModel
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class EmployeeInfo : MongoBaseInfo
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
    }
}
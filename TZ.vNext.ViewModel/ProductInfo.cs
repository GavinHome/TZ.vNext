//-----------------------------------------------------------------------------------
// <copyright file="SalaryInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 14:28:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.ViewModel
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class ProductInfo : MongoBaseInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DataStatusEnum DataStatus { get; set; }

        /// <summary>
        /// 更新账号
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateAt { get; set; }
    }
}
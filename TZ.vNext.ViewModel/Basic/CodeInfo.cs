//-----------------------------------------------------------------------------------
// <copyright file="CodeInfo.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;

namespace TZ.vNext.ViewModel
{
    public class CodeInfo : BaseInfo
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// CodeId
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// 类型Id
        /// </summary>
        public int CodeTypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}

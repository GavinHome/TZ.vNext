﻿//-----------------------------------------------------------------------
// <copyright file="RecordableAttribute.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>11/5/2017 10:52:33 PM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// 可记录修改日志的
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RecordableAttribute : Attribute
    {
        public RecordableAttribute(EntityState operationType = EntityState.Modified | EntityState.Deleted)
        {
            OperationType = operationType;
        }

        /// <summary>
        /// 可记录操作类型
        /// </summary>
        public EntityState OperationType { get; set; }
    }
}

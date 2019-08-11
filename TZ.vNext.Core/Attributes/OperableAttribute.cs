//-----------------------------------------------------------------------
// <copyright file="OperableAttribute.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>10/31/2017 11:40:42 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// 指示一个类型的操作类型, 默认为物理操作
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class OperableAttribute : Attribute
    {
        public OperableAttribute(OperationTypeEnum operationType = OperationTypeEnum.Insert | OperationTypeEnum.Update | OperationTypeEnum.Delete)
        {
            OperationType = operationType;
        }

        public OperationTypeEnum OperationType { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="NonRepeatableAttribute.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>11/5/2017 6:40:43 PM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// 指示一个操作是不可重复进行的
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class NonRepeatableAttribute : Attribute
    {
    }
}

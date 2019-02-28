//-----------------------------------------------------------------------
// <copyright file="NonRepeatableAttribute.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
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

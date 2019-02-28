//-----------------------------------------------------------------------
// <copyright file="NavigableAttribute.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
// <date>10/31/2017 11:40:42 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Attributes
{
    /// <summary>
    /// 指示一个导航属性是需要保留的
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NavigableAttribute : Attribute
    {
    }
}

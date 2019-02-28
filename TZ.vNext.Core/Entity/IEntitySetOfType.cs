//-----------------------------------------------------------------------
// <copyright file="IEntitySetOfType.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
// <date>7/27/2017 10:14:21 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

namespace TZ.vNext.Core.Entity
{
    public interface IEntitySetOfType<TKey> : IEntitySet
    {
        new TKey Id { get; set; }
    }
}

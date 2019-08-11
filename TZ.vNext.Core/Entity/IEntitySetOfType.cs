//-----------------------------------------------------------------------
// <copyright file="IEntitySetOfType.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
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

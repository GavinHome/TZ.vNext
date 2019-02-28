//-----------------------------------------------------------------------
// <copyright file="EntitySet.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
// <date>7/27/2017 10:14:21 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Core.Entity
{
    public abstract class EntitySet : IEntitySetOfType<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        object IEntitySet.Id => Id;
    }
}

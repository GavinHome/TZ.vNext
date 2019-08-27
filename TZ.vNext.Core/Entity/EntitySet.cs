//-----------------------------------------------------------------------
// <copyright file="EntitySet.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>7/27/2017 10:14:21 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ.vNext.Core.Entity
{
    public abstract class EntitySet : IEntitySetOfType<Guid>
    {
        [Key]
        [Description("唯一标识")]
        public Guid Id { get; set; }
        object IEntitySet.Id => Id;
    }
}

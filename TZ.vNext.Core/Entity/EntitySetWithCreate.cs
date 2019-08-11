//-----------------------------------------------------------------------
// <copyright file="EntitySetWithCreate.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>7/27/2017 10:19:45 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.Entity
{
    public abstract class EntitySetWithCreate : EntitySet
    {
        public DataStatusEnum DataStatus { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}

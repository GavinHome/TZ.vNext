//-----------------------------------------------------------------------
// <copyright file="EntitySetWithCreateAndUpdate.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>7/27/2017 10:26:20 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace TZ.vNext.Core.Entity
{
    public abstract class EntitySetWithCreateAndUpdate : EntitySetWithCreate
    {
        public string UpdateBy { get; set; }
        
        public DateTime? UpdateAt { get; set; }
    }
}

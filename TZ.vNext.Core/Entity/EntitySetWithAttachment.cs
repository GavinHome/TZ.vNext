//-----------------------------------------------------------------------
// <copyright file="EntitySetWithAttachment.cs" company="TZ.vNext">
//     Copyright TZ.vNext. All rights reserved.
// <author>??</author>
// <date>7/27/2017 10:19:45 AM</date>
// <description>缺少描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TZ.vNext.Core.Entity
{
    public abstract class EntitySetWithAttachment : EntitySet
    {
        public Guid? AttachmentId { get; set; }

        [NotMapped]
        public string FileName { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="EntitySetWithAttachment.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
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

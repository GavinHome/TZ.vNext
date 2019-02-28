//-----------------------------------------------------------------------
// <copyright file="EntitySetWithAllStatus.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>lilu</author>
// <date>2017/9/27 17:57:45</date>
// <description>描述</description>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.Entity
{
    public class EntitySetWithAllStatus : EntitySetWithCreateAndUpdate
    {
        public ApproveStatusEnum? ApprovalStatus { get; set; }

        [MaxLength(CommonConstant.DbStringFieldsLength64)]
        public string ApprovalStatusDescription { get; set; }

        //创建人部门
        public Guid? CreateByOrganizationId { get; set; }

        [NotMapped]
        public Guid BacklogId { get; set; }

        public DateTime? SubmitAt { get; set; }
    }
}

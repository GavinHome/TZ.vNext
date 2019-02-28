//-----------------------------------------------------------------------
// <copyright file="EntitySetWithCreateAndUpdate.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright www.tzecc.com. All rights reserved.
// <author>苟向阳</author>
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

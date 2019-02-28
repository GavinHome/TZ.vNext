//-----------------------------------------------------------------------------------
// <copyright file="EntitySetExtensions.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.Extensions
{
    public static class EntitySetExtensions
    {
        public static void SetEntityPrincipal(this object value, System.Security.Claims.ClaimsPrincipal user)
        {            
            var code = user.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var organId = user.FindFirst(c => c.Type == JwtClaimNamesConst.Org);
            if (value is EntitySet t)
            {
                var isNew = t.IsNullOrEmpty();
                if (value is EntitySetWithCreate esc)
                {
                    if (isNew)
                    {
                        esc.CreateAt = DateTime.Now;
                        esc.CreateBy = code?.Value;
                        esc.DataStatus = DataStatusEnum.Valid;
                    }

                    if (value is EntitySetWithCreateAndUpdate escu)
                    {
                        escu.UpdateAt = DateTime.Now;
                        escu.UpdateBy = code?.Value;
                    }

                    if (value is EntitySetWithAllStatus ess)
                    {
                        if (isNew)
                        {
                            Guid? organizationId = null;
                            if (!string.IsNullOrEmpty(organId?.Value) && Guid.TryParse(organId?.Value, out Guid tempId))
                            {
                                organizationId = tempId;
                            }
                            
                            ess.CreateByOrganizationId = organizationId;
                        }

                        if (ess.ApprovalStatus == ApproveStatusEnum.Auditing)
                        {
                            ess.SubmitAt = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}

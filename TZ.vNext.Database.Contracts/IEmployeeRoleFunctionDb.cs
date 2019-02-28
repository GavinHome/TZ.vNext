//-----------------------------------------------------------------------------------
// <copyright file="IEmployeeRoleFunctionDb.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/13 17:29:11</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TZ.vNext.Database.Contracts
{
    /// <summary>
    /// 人员角色权限
    /// </summary>
    public interface IEmployeeRoleFunctionDb : IDbCommon
    {
        /// <summary>
        /// 根据用户获取权限
        /// </summary>
        /// <param name="userId">用户</param>
        /// <returns>用户权限</returns>
        IList<Guid> GetFunctionsByUserId(Guid userId);
    }
}

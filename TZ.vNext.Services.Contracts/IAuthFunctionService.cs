//-----------------------------------------------------------------------------------
// <copyright file="IAuthFunctionService.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TZ.vNext.Services.Contracts
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public interface IAuthFunctionService
    {
        /// <summary>
        /// 根据账号获取员工权限
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工权限</returns>
        IList<Guid> GetFunctionsByUserName(string userName);
    }
}

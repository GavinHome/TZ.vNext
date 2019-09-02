//-----------------------------------------------------------------------------------
// <copyright file="IAuthFunctionService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
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
        IList<string> GetFunctionsByUserName(string userName);
    }
}

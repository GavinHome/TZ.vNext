﻿//-----------------------------------------------------------------------------------
// <copyright file="IEmployeeDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/26 16:47:28</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using TZ.vNext.Model;

namespace TZ.vNext.Database.Contracts
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public interface IEmployeeDb : IMongoDbCommon
    {
        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工信息</returns>
        Employee FindByUserName(string userName);
    }
}

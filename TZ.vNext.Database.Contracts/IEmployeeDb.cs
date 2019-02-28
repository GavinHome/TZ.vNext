//-----------------------------------------------------------------------------------
// <copyright file="IEmployeeDb.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/26 16:47:28</date>
// <description></description>
//-----------------------------------------------------------------------------------

using TZ.vNext.Model;

namespace TZ.vNext.Database.Contracts
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public interface IEmployeeDb : IDbCommon
    {
        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工信息</returns>
        VEmployee FindByUserName(string userName);
    }
}

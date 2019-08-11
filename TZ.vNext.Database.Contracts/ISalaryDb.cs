//-----------------------------------------------------------------------------------
// <copyright file="ISalaryDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2018/12/3 17:29:11</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using TZ.vNext.Model;

namespace TZ.vNext.Database.Contracts
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public interface ISalaryDb : IDbCommon
    {
        /// <summary>
        /// 根据名称获取薪酬项
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>薪酬项信息</returns>
        Task<Salary> FindByName(string name);

        /// <summary>
        /// 根据key获取薪酬项
        /// </summary>
        /// <param name="keys">薪酬项的key</param>
        /// <returns>薪酬项信息</returns>
        Task<List<Salary>> FindByKeys(List<string> keys);
    }
}

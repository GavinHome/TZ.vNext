//-----------------------------------------------------------------------------------
// <copyright file="IFormDataService.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/27 15:48:47</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections;

namespace TZ.vNext.Services.Contracts
{
    /// <summary>
    /// 提供公共数据服务
    /// </summary>
    public interface IFormDataService
    {
        /// <summary>
        /// 获取数据源（通用）
        /// </summary>
        /// <returns>数据源</returns>
        IEnumerable GridQueryDataSourceMeta();

        /// <summary>
        /// 获取数据源元信息（通用）
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>数据源元信息</returns>
        IEnumerable GridQuerySchema(string key);

        /// <summary>
        /// 获取枚举数据（通用）
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>枚举数据</returns>
        IEnumerable GridQueryEnumType(string key);
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="IMongoDbCommon.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 10:21:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

namespace TZ.vNext.Database.Contracts
{
    /// <summary>
    /// Mongo Db Common
    /// </summary>
    public interface IMongoDbCommon : IDbCommon
    {
        /// <summary>
        /// 生成新Id
        /// </summary>
        /// <returns>Id</returns>
        string NewId();

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="TModel">TModel</typeparam>
        /// <param name="model">model</param>
        /// <returns>Model</returns>
        TModel Add<TModel>(TModel model);
    }
}

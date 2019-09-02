//-----------------------------------------------------------------------------------
// <copyright file="ProductDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>yangxiaomin</author>
// <date>2019/08/31 10:15:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using TZ.vNext.Core.Mongo.Context;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model.Context;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class ProductDb : MongoDbCommon, IProductDb
    {
        public ProductDb(MongoContext dbcontext) : base(dbcontext)
        {
        }
    }
}

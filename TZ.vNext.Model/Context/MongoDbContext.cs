//-----------------------------------------------------------------------------------
// <copyright file="MongoDbContext.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/22 16:53:32</date>
// <description></description>
//-----------------------------------------------------------------------------------

using TZ.vNext.Core.Mongo;
using TZ.vNext.Core.Mongo.Context;

namespace TZ.vNext.Model.Context
{
    public class MongoDbContext : MongoContext
    {
        public MongoDbContext(MongoDbContextOptions options) : base(options)
        {
        }

        public MongoDbContext()
        {
        }
    }
}

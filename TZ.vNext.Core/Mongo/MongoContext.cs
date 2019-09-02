//-----------------------------------------------------------------------------------
// <copyright file="MongoContext.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/02 15:04:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using MongoDB.Driver;
using TZ.vNext.Core.Mongo.Extensions;

namespace TZ.vNext.Core.Mongo.Context
{
    public class MongoContext
    {
        private MongoDbContextOptions options;

        public MongoContext(MongoDbContextOptions options)
        {
            this.options = options;
        }

        public MongoContext()
        {
        }

        protected IMongoDatabase DBSession
        {
            get
            {
                return MongoSessionManager.GetDBSession(options.ConnectString, options.Database);
            }
        }

        public virtual IMongoCollection<T> Of<T>()
        {
            return DBSession.Get<T>();
        }

        public virtual Task<IMongoCollection<T>> OfAsync<T>()
        {
            return DBSession.GetAsync<T>();
        }
    }
}

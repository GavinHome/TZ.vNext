//-----------------------------------------------------------------------------------
// <copyright file="MongoContext.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using MongoDB.Driver;
using TZ.vNext.Core.Mongo.Extensions;
using TZ.vNext.Model.Mongo;

namespace TZ.vNext.Model.Context
{
    public class MongoContext
    {
        protected IMongoDatabase DBSession
        {
            get
            {
                return MongoSessionManager.GetDBSession();
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

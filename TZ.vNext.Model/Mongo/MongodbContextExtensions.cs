//-----------------------------------------------------------------------------------
// <copyright file="MongodbContextExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 9:16:50</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Model.Mongo
{
    public static class MongodbContextExtensions
    {
        public static IMongoCollection<T> Get<T>(this IMongoDatabase DBSession)
        {
            GuardUtils.NotNull(DBSession, nameof(DBSession));
            string tableName = BsonClassMap.LookupClassMap(typeof(T)).Discriminator;
            return DBSession.GetCollection<T>(tableName);
        }

        public static async Task<IMongoCollection<T>> GetAsync<T>(this IMongoDatabase DBSession)
        {
            string tableName = BsonClassMap.LookupClassMap(typeof(T)).Discriminator;
            return await Task.Run(() => DBSession.GetCollection<T>(tableName));
        }
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="MongodbContextExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/14 9:16:50</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TZ.vNext.Core.Const;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Utility;
using TZ.vNext.Model.Mongo.Entity;

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

        public static void SetEntityPrincipal(this MongoDbEntity entity, System.Security.Claims.ClaimsPrincipal user)
        {
            var code = user.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var isNew = string.IsNullOrEmpty(entity.Id) || System.Guid.Empty.ToString() == entity.Id;
            var name = user.FindFirst(c => c.Type == JwtClaimNamesConst.UseName);

            if (entity is MongoDbEntity esc)
            {
                if (isNew)
                {
                    esc.Id = System.Guid.NewGuid().ToString();
                    esc.CreateAt = DateTime.Now;
                    esc.CreateBy = code?.Value;
                    esc.DataStatus = DataStatusEnum.Valid;
                }

                if (entity is MongoDbEntityWithCreateAndByName ewcn)
                {
                    ewcn.CreateByName = name?.Value;
                }

                if (entity is MongoDbEntityWithUpdate escu)
                {
                    escu.UpdateAt = DateTime.Now;
                    escu.UpdateBy = code?.Value;
                }

                if (entity is MongoDbEntityWithUpdateAndByName esun)
                {
                    esun.CreateByName = name?.Value;
                    esun.UpdateByName = name?.Value;
                }
            }
        }
    }
}

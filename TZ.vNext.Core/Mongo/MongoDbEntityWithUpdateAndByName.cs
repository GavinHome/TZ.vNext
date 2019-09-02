//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntityWithUpdateAndByName.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 14:05:46</date>
// <description></description>
//-----------------------------------------------------------------------------------
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TZ.vNext.Core.Mongo.Entity
{
    public abstract class MongoDbEntityWithUpdateAndByName : MongoDbEntityWithUpdate
    {
        [BsonRepresentation(BsonType.String)]
        public virtual string CreateByName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public virtual string UpdateByName { get; set; }
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntityWithUpdate.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 14:05:46</date>
// <description></description>
//-----------------------------------------------------------------------------------
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TZ.vNext.Model.Mongo.Entity
{
    public abstract class MongoDbEntityWithUpdate : MongoDbEntity
    {
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public virtual DateTime UpdateAt { get; set; }

        [BsonRepresentation(BsonType.String)]
        public virtual string UpdateBy { get; set; }
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntityWithCreateAndByName.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 14:05:46</date>
// <description></description>
//-----------------------------------------------------------------------------------
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TZ.vNext.Core.Mongo.Entity
{
    public abstract class MongoDbEntityWithCreateAndByName : MongoDbEntity
    {
        [BsonRepresentation(BsonType.String)]
        public virtual string CreateByName { get; set; }
    }
}

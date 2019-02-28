//-----------------------------------------------------------------------------------
// <copyright file="MongoDbEntity.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Entity;

namespace TZ.vNext.Model.Mongo.Entity
{
    public abstract class MongoDbEntity : IEntitySetOfType<string>
    {
        [BsonRequired]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public virtual string Id { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public virtual DataStatusEnum DataStatus { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public virtual DateTime CreateAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public virtual DateTime UpdateAt { get; set; }

        /// <summary>
        /// 额外元素，所有未包含在映射中元素会存在于此,类型可以为：IDictionary_string, object 或 BsonDocument
        /// </summary>
        [BsonExtraElements]
        public virtual IDictionary<string, object> CatchAll { get; set; }

        object IEntitySet.Id => Id;
    }
}

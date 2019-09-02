//-----------------------------------------------------------------------------------
// <copyright file="Salary.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 10:00:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TZ.vNext.Core.Mongo.Entity;

namespace TZ.vNext.Model
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    [Table("Product_Info")]
    public class Product : MongoDbEntityWithUpdateAndByName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public BsonDocument Content { get; set; }

        /// <summary>
        /// 内容json
        /// </summary>
        [BsonIgnore]
        public dynamic ContentData
        {
            get
            {
                return Content == null ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(Content.ToJson());
            }
        }
    }
}
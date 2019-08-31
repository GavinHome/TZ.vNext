//-----------------------------------------------------------------------------------
// <copyright file="Salary.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 10:00:01</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using TZ.vNext.Model.Mongo.Entity;

namespace TZ.vNext.Model
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    [Table("Product_Info")]
    public class Product : MongoDbEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
}
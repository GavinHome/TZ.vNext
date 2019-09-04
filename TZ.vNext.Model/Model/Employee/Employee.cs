//-----------------------------------------------------------------------------------
// <copyright file="Employee.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>yangxiaomin</author>
// <date>2019/09/02 17:15:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Core.Mongo.Entity;

namespace TZ.vNext.Model
{
    /// <summary>
    /// Employee
    /// </summary>
    [BsonDiscriminator("Employee")]
    [DataSource("员工数据", "/api/Employee/GridQueryEmployees")]
    public class Employee : MongoDbEntityWithUpdateAndByName
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Description("账号")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        [Description("组织机构")]
        public string OrganizationId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public IList<string> Functions { get; set; }
    }
}

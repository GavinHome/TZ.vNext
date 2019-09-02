//-----------------------------------------------------------------------------------
// <copyright file="MongoDbContextOptionsBuilder.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/02 15:04:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace TZ.vNext.Core.Mongo
{
    public class MongoDbContextOptionsBuilder
    {
        public MongoDbContextOptionsBuilder()
        {
            Options = new MongoDbContextOptions();
        }

        //
        // 摘要:
        //     Gets the options being configured.
        public virtual MongoDbContextOptions Options { get; }

        public MongoDbContextOptionsBuilder UseMongoServer(string connectionString)
        {
            //Data Source=mongodb://127.0.0.1:27017;Database=tzsalary
            if (string.IsNullOrEmpty(connectionString) || !connectionString.ToLower().Contains("Data Source=".ToLower()) || !connectionString.ToLower().Contains("Database=".ToLower()))
            {
                throw new ArgumentNullException(nameof(connectionString), "mongo配置错误或连接串为空：MongodbConnection，示例：Data Source=mongodb://127.0.0.1:27017;Database=local");
            }

            var client = connectionString.Split(";").FirstOrDefault(x => x.Contains("Data Source=") || x.Contains("data source=")).Replace("Data Source=", string.Empty).Replace("data Source=", string.Empty).TrimStart().TrimEnd();

            if (string.IsNullOrEmpty(client))
            {
                throw new ArgumentNullException(nameof(connectionString), "mongo配置错误：Data Source，示例：Data Source=mongodb://127.0.0.1:27017;Database=local");
            }

            var database = connectionString.Split(";").FirstOrDefault(x => x.Contains("Database=") || x.Contains("database=")).Replace("Database=", string.Empty).Replace("database=", string.Empty).TrimStart().TrimEnd();

            if (string.IsNullOrEmpty(database))
            {
                throw new ArgumentNullException(nameof(connectionString), "mongo配置错误：Database，示例：Data Source=mongodb://127.0.0.1:27017;Database=local");
            }

            Options.ConnectString = client;
            Options.Database = database;

            return this;
        }
    }
}

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
        private readonly string SettingSample = "Data Source=mongodb://127.0.0.1:27017;Database=local";
        private readonly string DataSourceKey = "Data Source";
        private readonly string DatabaseKey = "Database";

        public MongoDbContextOptionsBuilder()
        {
            Options = new MongoDbContextOptions() { ConnectString = "mongodb://127.0.0.1:27017", Database = "local" };
        }

        public virtual MongoDbContextOptions Options { get; }

        public MongoDbContextOptionsBuilder UseMongoServer(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || !connectionString.ToLower().Contains($"{DataSourceKey}=".ToLower()) || !connectionString.ToLower().Contains($"{DatabaseKey}=".ToLower()))
            {
                throw new ArgumentNullException(nameof(connectionString), $"mongo配置错误或连接串为空：MongodbConnection，示例：{SettingSample}");
            }

            var client = connectionString.Split(";").FirstOrDefault(x => x.Contains($"{DataSourceKey}=") || x.Contains($"{DataSourceKey.ToLower()}=")).Replace($"{DataSourceKey}=", string.Empty).Replace($"{DataSourceKey.ToLower()}=", string.Empty).TrimStart().TrimEnd();

            if (string.IsNullOrEmpty(client))
            {
                throw new ArgumentNullException(nameof(connectionString), $"mongo配置错误：{DataSourceKey}，示例：{SettingSample}");
            }

            var database = connectionString.Split(";").FirstOrDefault(x => x.Contains($"{DatabaseKey}=") || x.Contains($"{DatabaseKey.ToLower()}=")).Replace($"{DatabaseKey}=", string.Empty).Replace($"{DatabaseKey.ToLower()}= ", string.Empty).TrimStart().TrimEnd();

            if (string.IsNullOrEmpty(database))
            {
                throw new ArgumentNullException(nameof(connectionString), $"mongo配置错误：{DatabaseKey}，示例：{SettingSample}");
            }

            Options.ConnectString = client;
            Options.Database = database;

            return this;
        }

        ////private MongoDbContextOptionsBuilder UseMongoServer(IMongoClient client)
        ////{
        ////    return this;
        ////}

        ////private MongoDbContextOptionsBuilder UseDatabase(string name)
        ////{
        ////    client.GetDatabase(name);
        ////    return this;
        ////}

        ////private MongoDbContextOptionsBuilder UseMongoServer(IMongoDatabase database)
        ////{
        ////    return this;
        ////}
    }
}

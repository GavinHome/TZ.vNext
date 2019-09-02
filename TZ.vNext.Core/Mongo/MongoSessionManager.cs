//-----------------------------------------------------------------------------------
// <copyright file="MongoSessionManager.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/02 15:04:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using MongoDB.Driver;

namespace TZ.vNext.Core.Mongo
{
    public static class MongoSessionManager
    {
        private static IMongoDatabase _mongoDatabase = null;
        private static object _locker = new object();

        public static IMongoDatabase GetDBSession(string client, string database)
        {
            if (_mongoDatabase == null)
            {
                ////单实例对象构造
                lock (_locker)
                {
                    if (_mongoDatabase == null)
                    {
                        if (string.IsNullOrEmpty(client) || string.IsNullOrEmpty(database))
                        {
                            throw new System.ArgumentException("mongo配置不正确或没有配置mongo连接串");
                        }
                        else
                        {
                            _mongoDatabase = new MongoClient(client).GetDatabase(database);
                        }
                    }
                }
            }

            return _mongoDatabase;
        }
    }
}

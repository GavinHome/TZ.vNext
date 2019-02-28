//-----------------------------------------------------------------------------------
// <copyright file="MongoSessionManager.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/22 16:54:52</date>
// <description></description>
//-----------------------------------------------------------------------------------

using MongoDB.Driver;
using TZ.vNext.Core.Const;

namespace TZ.vNext.Model.Mongo
{
    public static class MongoSessionManager
    {
        private static IMongoDatabase _mongoDatabase = null;
        private static object _locker = new object();

        public static IMongoDatabase GetDBSession()
        {
            if (_mongoDatabase == null)
            {
                ////单实例对象构造
                lock (_locker)
                {
                    if (_mongoDatabase == null)
                    {
                        if (string.IsNullOrEmpty(SystemVariableConst.MongoDB_Path) || string.IsNullOrEmpty(SystemVariableConst.MongoDB_Name))
                        {
                            throw new System.ArgumentException("mongo配置不正确或没有配置mongo连接串");
                        }
                        else
                        {
                            _mongoDatabase = new MongoClient(SystemVariableConst.MongoDB_Path).GetDatabase(SystemVariableConst.MongoDB_Name);
                        }
                    }
                }
            }

            return _mongoDatabase;
        }
    }
}

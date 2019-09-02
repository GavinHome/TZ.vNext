//-----------------------------------------------------------------------------------
// <copyright file="EmployeeDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/26 16:48:13</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using TZ.vNext.Core.Mongo.Context;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeDb : MongoDbCommon, IEmployeeDb
    {
        public EmployeeDb(MongoContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工信息</returns>
        public Employee FindByUserName(string userName)
        {
            return Get<Employee>().Where(x => x.UserName == userName).FirstOrDefault();
        }
    }
}

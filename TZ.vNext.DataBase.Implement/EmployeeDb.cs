//-----------------------------------------------------------------------------------
// <copyright file="EmployeeDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/26 16:48:13</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Model.Context;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeDb : DbCommon, IEmployeeDb
    {
        public EmployeeDb(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据账号获取员工信息
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工信息</returns>
        public VEmployee FindByUserName(string userName)
        {
            if (userName == "admin")
            {
                return new VEmployee
                {
                    Id = Guid.Empty,
                    Code = "admin",
                    Name = "管理员",
                    UserName = "admin",
                    Password = MD5Helper.MD5UserPassword("admin", "123"),
                    OrganizationId = Guid.Parse("20000000-0000-0000-0000-000000000039")
                };
            }

            var result = new VEmployee
            {
                Id = Guid.NewGuid(),
                Code = "201900666",
                Name = "王麻子",
                UserName = "201900666",
                Password = MD5Helper.MD5UserPassword("201900666", "1"),
                OrganizationId = Guid.Parse("20000000-0000-0000-0000-000000000039")
            };

            return result;
        }
    }
}

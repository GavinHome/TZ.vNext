//-----------------------------------------------------------------------------------
// <copyright file="AuthFunctionService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Services.Contracts;

namespace TZ.vNext.Services.Implement
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class AuthFunctionService : IAuthFunctionService
    {
        private readonly IServiceScopeFactory scopeFactory;
        public AuthFunctionService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 根据账号获取员工权限
        /// </summary>
        /// <param name="userName">账号</param>
        /// <returns>员工权限</returns>
        public IList<Guid> GetFunctionsByUserName(string userName)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                ////var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var employeeRoleFunctionDb = scope.ServiceProvider.GetRequiredService<IEmployeeRoleFunctionDb>();
                var employeeDb = scope.ServiceProvider.GetRequiredService<IEmployeeDb>();
                ////var user = await db.AsQueryable<Employee>().FirstOrDefaultAsync(x => x.UserName == userName);
                var user = employeeDb.FindByUserName(userName);
                var result = employeeRoleFunctionDb.GetFunctionsByUserId(user.Id);
                return result;
            }
        }
    }
}

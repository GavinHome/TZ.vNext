﻿//-----------------------------------------------------------------------------------
// <copyright file="EmployeeRoleFunctionDb.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 13:26:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model.Context;
using TZ.vNext.Model;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// 人员角色权限
    /// </summary>
    public class EmployeeRoleFunctionDb : DbCommon, IEmployeeRoleFunctionDb
    {
        public EmployeeRoleFunctionDb(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据用户获取权限
        /// </summary>
        /// <param name="userId">用户</param>
        /// <returns>用户权限</returns>
        public IList<Guid> GetFunctionsByUserId(Guid userId)
        {
            var result = new List<Guid>
            {
                Guid.Parse("00000000-0000-1111-0000-000000000000"),
                Guid.Parse("00000000-0000-1111-1000-000000000000"),
                Guid.Parse("00000000-0000-1111-1005-000000000000")
            };

            return result;
        }
    }
}

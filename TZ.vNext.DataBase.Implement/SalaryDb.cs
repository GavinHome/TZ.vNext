//-----------------------------------------------------------------------------------
// <copyright file="SalaryDb.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2018/11/26 13:26:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TZ.vNext.Core.Enum;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Model.Context;
using TZ.vNext.Model.Enum;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class SalaryDb : DbCommon, ISalaryDb
    {
        public SalaryDb(AppDbContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据名称获取薪酬项
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>薪酬项信息</returns>
        public async Task<Salary> FindByName(string name)
        {
            var model = (await GetAsync<Salary>())
                          .AsNoTracking()
                          .SingleOrDefault(x => x.Name == name);
            return model;
        }

        /// <summary>
        /// 根据key获取薪酬项
        /// </summary>
        /// <param name="keys">薪酬项的key</param>
        /// <returns>薪酬项信息</returns>
        public async Task<List<Salary>> FindByKeys(List<string> keys)
        {
            var model = (await GetAsync<Salary>())
                .AsNoTracking()
                .Where(x => keys.Contains(x.Key)).ToList();
            return model;
        }
    }
}

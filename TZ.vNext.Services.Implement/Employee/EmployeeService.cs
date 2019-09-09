//-----------------------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>yangxiaomin</author>
// <date>2019/09/02 17:15:42</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Services.Contracts;

namespace TZ.vNext.Services.Implement
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDb _employeeDb;

        public EmployeeService(IEmployeeDb employeeDb)
        {
            _employeeDb = employeeDb;
        }

        public async Task<IQueryable<Employee>> GetAllValidUserQuery()
        {
            var query = (await _employeeDb.GetAsync<Employee>()).Where(x => x.DataStatus == Core.Enum.DataStatusEnum.Valid);
            return query;
        }

        public async Task<IList<Employee>> Init()
        {
            var list = new List<Employee>();
            list.Add(new Employee()
            {
                Id = _employeeDb.NewId(),
                Code = "admin",
                Name = "管理员",
                UserName = "admin",
                Password = MD5Helper.MD5UserPassword("admin", "1"),
                OrganizationId = "20000000-0000-0000-0000-000000000039",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CreateBy = "Initialized",
                UpdateBy = "Initialized",
                CreateByName = "Initialized",
                UpdateByName = "Initialized",
                DataStatus = Core.Enum.DataStatusEnum.Valid,
                Functions = new List<string>()
                  {
                      "00000000-0000-1112-0000-000000000000",
                      "00000000-0000-1112-0001-000000000000"
                  }
            });

            list.Add(new Employee()
            {
                Id = _employeeDb.NewId(),
                Code = "201900666",
                Name = "王麻子",
                UserName = "201900666",
                Password = MD5Helper.MD5UserPassword("201900666", "1"),
                OrganizationId = "20000000-0000-0000-0000-000000000039",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CreateBy = "Initialized",
                UpdateBy = "Initialized",
                CreateByName = "Initialized",
                UpdateByName = "Initialized",
                DataStatus = Core.Enum.DataStatusEnum.Valid,
                Functions = new List<string>()
                  {
                      "00000000-0000-1111-0000-000000000000",
                      "00000000-0000-1111-1000-000000000000",
                      "00000000-0000-1111-1005-000000000000"
                  }
            });

            var updates = new List<Employee>();
            foreach (var item in list)
            {
                var user = _employeeDb.FindByUserName(item.UserName);
                if (user == null)
                {
                    updates.Add(item);
                }
                else
                {
                    user.Code = item.Code;
                    user.Name = item.Name;
                    user.UserName = item.UserName;
                    user.Password = MD5Helper.MD5UserPassword(item.Code, "1");
                    user.OrganizationId = item.OrganizationId;
                    user.UpdateAt = DateTime.Now;
                    user.DataStatus = Core.Enum.DataStatusEnum.Valid;
                    user.Functions = item.Functions;
                }
            }

            return await _employeeDb.BatchSaveOrUpdateAsync<Employee>(updates);
        }
    }
}

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
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Mongo.Extensions;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Implement
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDb _employeeDb;

        public EmployeeService(IEmployeeDb employeeDb)
        {
            _employeeDb = employeeDb;
        }

        public async Task<IQueryable<Employee>> GetAllValid()
        {
            var query = (await _employeeDb.GetAsync<Employee>()).Where(x => x.DataStatus == DataStatusEnum.Valid);
            return query;
        }

        public async Task<bool> Disable(string id)
        {
            var model = await _employeeDb.GetAsync<Employee>(id);
            model.DataStatus = DataStatusEnum.Invalid;
            await _employeeDb.UpdateAsync<Employee>(model);
            return true;
        }

        public async Task<bool> Enable(string id)
        {
            var model = await _employeeDb.GetAsync<Employee>(id);
            model.DataStatus = DataStatusEnum.Valid;
            await _employeeDb.UpdateAsync<Employee>(model);
            return true;
        }

        public async Task<EmployeeInfo> FindByCode(string code)
        {
            return await Task.Run(() => _employeeDb.FindByUserName(code).ToViewModel<EmployeeInfo>());
        }

        public async Task<object> FindById(string id)
        {
            var model = await _employeeDb.GetAsync<Employee>(id);
            return model.ToViewModel<EmployeeInfo>();
        }

        public async Task<EmployeeInfo> Save(EmployeeInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));

            var codeModel = _employeeDb.FindByUserName(info.Code);
            if (codeModel != null && codeModel.Id != info.Id)
            {
                return null;
            }

            Employee model = info.Id != string.Empty ? await _employeeDb.GetAsync<Employee>(info.Id) : null;
            if (model != null)
            {
                model.Name = info.Name;
                model.Code = info.Code;
                model.UserName = info.Code;
                if (model.Code != info.Code)
                {
                    model.Password = MD5Helper.MD5UserPassword(model.Code, "1");
                }

                model.SetEntityPrincipal(info.User);
                model = await _employeeDb.UpdateAsync<Employee>(model);
            }
            else
            {
                model = info.ToModel<Employee>();
                model.Id = _employeeDb.NewId();
                model.OrganizationId = "20000000-0000-0000-0000-000000000039";
                model.Functions = new List<string>()
                {
                   "00000000-0000-1112-0000-000000000000",
                   "00000000-0000-1112-0001-000000000000"
                };

                model.UserName = model.Code;
                model.Password = MD5Helper.MD5UserPassword(model.Code, "1");
                model.SetEntityPrincipal(info.User);
                model = await _employeeDb.SaveAsync<Employee>(model);
            }

            return model.ToViewModel<EmployeeInfo>();
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
                      "00000000-0000-1112-0001-000000000000",
                      "00000000-0000-1113-0000-000000000000",
                      "00000000-0000-1113-0001-000000000000"
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
                      //"00000000-0000-1111-0000-000000000000",
                      //"00000000-0000-1111-1000-000000000000",
                      //"00000000-0000-1111-1005-000000000000",
                      "00000000-0000-1112-0000-000000000000",
                      "00000000-0000-1112-0001-000000000000",
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
                    updates.Add(user);
                }
            }

            return await _employeeDb.BatchSaveOrUpdateAsync<Employee>(updates);
        }

        public async Task<IQueryable<Employee>> Get()
        {
            return await _employeeDb.GetAsync<Employee>();
        }
    }
}

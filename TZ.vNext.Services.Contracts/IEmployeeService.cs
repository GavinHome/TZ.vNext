//-----------------------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="TZ.vNext">
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
using TZ.vNext.Model;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IList<Employee>> Init();
        Task<IQueryable<Employee>> GetAllValid();
        Task<object> FindById(string id);
        Task<bool> Disable(string id);
        Task<bool> Enable(string id);
        Task<EmployeeInfo> Save(EmployeeInfo info);
        Task<EmployeeInfo> FindByCode(string code);
        Task<IQueryable<Employee>> Get();
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="SalaryService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/17 21:48:05</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Model.Enum;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Implement
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryDb _salaryDb;

        public SalaryService(ISalaryDb salaryDb)
        {
            _salaryDb = salaryDb;
        }

        /// <summary>
        /// 获取薪酬项
        /// </summary>
        /// <returns>薪酬项</returns>
        public IQueryable<Salary> Get()
        {
            return _salaryDb.Get<Salary>().OrderByDescending(x => x.CreateAt);
        }

        /// <summary>
        /// 保存薪酬项
        /// </summary>
        /// <param name="info">薪酬项</param>
        /// <returns>薪酬项信息</returns>
        public async Task<SalaryInfo> Save(SalaryInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));
            var model = info.ToModel<Salary>();
            if (info.Id != Guid.Empty)
            {
                model = await _salaryDb.UpdateAsync<Salary>(model);
            }
            else
            {
                model.OrderIndex = _salaryDb.Get<Salary>().Max(x => x.OrderIndex) + 1;
                model = await _salaryDb.SaveAsync<Salary>(model);
            }

            return model.ToViewModel<SalaryInfo>();
        }

        /// <summary>
        /// 删除薪酬项
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(Guid id)
        {
            var model = await _salaryDb.GetAsync<Salary>(id);
            return await _salaryDb.DeleteAsync<Salary>(model);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Enable(Guid id)
        {
            var model = await _salaryDb.GetAsync<Salary>(id);
            model.DataStatus = DataStatusEnum.Valid;
            await _salaryDb.UpdateAsync<Salary>(model);
            return true;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Disable(Guid id)
        {
            var model = await _salaryDb.GetAsync<Salary>(id);
            model.DataStatus = DataStatusEnum.Invalid;
            await _salaryDb.UpdateAsync<Salary>(model);
            return true;
        }

        /// <summary>
        /// 获取薪酬项
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>薪酬项</returns>
        public async Task<SalaryInfo> FindById(Guid id)
        {
            var model = await _salaryDb.GetAsync<Salary>(id);
            return model.ToViewModel<SalaryInfo>();
        }

        /// <summary>
        /// 根据名称获取薪酬项
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>薪酬项</returns>
        public async Task<SalaryInfo> FindByName(string name)
        {
            return (await _salaryDb.FindByName(name)).ToViewModel<SalaryInfo>();
        }

        /// <summary>
        /// 获取可用薪酬项
        /// </summary>
        /// <returns>可用薪酬项</returns>
        public async Task<IList<SalaryInfo>> GetAvaliable()
        {
            return await Task.Run(() => _salaryDb.Get<Salary>().Where(x => x.DataStatus == DataStatusEnum.Valid && x.FormType != FormType.Formula && x.Key != null && x.Name != null).ToList().ToViewModels<Salary, SalaryInfo>());
        }
    }
}
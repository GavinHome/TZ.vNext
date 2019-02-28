//-----------------------------------------------------------------------------------
// <copyright file="ISalaryService.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/7 13:09:47</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Contracts
{
    /// <summary>
    /// 薪酬项
    /// </summary>
    public interface ISalaryService
    {
        /// <summary>
        /// 获取薪酬项
        /// </summary>
        /// <returns>薪酬项</returns>
        IQueryable<Model.Salary> Get();

        /// <summary>
        /// 保存薪酬项
        /// </summary>
        /// <param name="info">薪酬项</param>
        /// <returns>薪酬项信息</returns>
        Task<SalaryInfo> Save(SalaryInfo info);

        /// <summary>
        /// 删除薪酬项
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        Task<bool> Enable(Guid id);

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>是否成功</returns>
        Task<bool> Disable(Guid id);

        /// <summary>
        /// 获取薪酬项
        /// </summary>
        /// <param name="id">薪酬项id</param>
        /// <returns>薪酬项</returns>
        Task<SalaryInfo> FindById(Guid id);

        /// <summary>
        /// 根据名称获取薪酬项
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>薪酬项</returns>
        Task<SalaryInfo> FindByName(string name);

        /// <summary>
        /// 获取可用薪酬项
        /// </summary>
        /// <returns>可用薪酬项</returns>
        Task<IList<SalaryInfo>> GetAvaliable();
    }
}

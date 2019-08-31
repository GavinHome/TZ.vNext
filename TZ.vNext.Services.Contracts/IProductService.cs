//-----------------------------------------------------------------------------------
// <copyright file="IProductService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 09:54:47</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Contracts
{
    /// <summary>
    /// 产品
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 获取我的产品列表
        /// </summary>
        /// <returns>产品列表</returns>
        IQueryable<Model.Product> Get();

        /// <summary>
        /// 保存产品
        /// </summary>
        /// <param name="info">产品</param>
        /// <returns>产品信息</returns>
        Task<ProductInfo> Save(ProductInfo info);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否成功</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否成功</returns>
        Task<bool> Enable(Guid id);

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否成功</returns>
        Task<bool> Disable(Guid id);

        /// <summary>
        /// 获取我的产品信息（单条）
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>产品信息</returns>
        Task<ProductInfo> FindById(Guid id);
    }
}

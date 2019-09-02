//-----------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/08/31 10:05:35</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TZ.vNext.Core.Mongo.Extensions;
using TZ.vNext.Core.Enum;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model;
using TZ.vNext.Model.Mongo;
using TZ.vNext.Services.Contracts;
using TZ.vNext.ViewModel;

namespace TZ.vNext.Services.Implement
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductDb _productDb;

        public ProductService(IProductDb productDb)
        {
            _productDb = productDb;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns>产品列表</returns>
        public IQueryable<Product> Get()
        {
            return _productDb.Get<Product>().Where(x => x.DataStatus == DataStatusEnum.Valid).OrderByDescending(x => x.UpdateAt);
        }

        /// <summary>
        /// 保存产品
        /// </summary>
        /// <param name="info">产品</param>
        /// <returns>产品信息</returns>
        public async Task<ProductInfo> Save(ProductInfo info)
        {
            GuardUtils.NotNull(info, nameof(info));
            Product model = info.Id != string.Empty ? await _productDb.GetAsync<Product>(info.Id) : null;
            if (model != null)
            {
                model.Name = info.Name;
                model.Description = info.Description;
                model.Content = info.ContentData != null ? MongoDB.Bson.BsonDocument.Parse(info.ContentData.ToString()) : null;
                model.SetEntityPrincipal(info.User);
                model = await _productDb.UpdateAsync<Product>(model);
            }
            else
            {
                model = info.ToModel<Product>();
                model.SetEntityPrincipal(info.User);
                model = await _productDb.SaveAsync<Product>(model);
            }

            return model.ToViewModel<ProductInfo>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Delete(Guid id)
        {
            var model = await _productDb.GetAsync<Product>(id);
            return await _productDb.DeleteAsync<Product>(model);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Enable(Guid id)
        {
            var model = await _productDb.GetAsync<Product>(id);
            model.DataStatus = DataStatusEnum.Valid;
            await _productDb.UpdateAsync<Product>(model);
            return true;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>是否成功</returns>
        public async Task<bool> Disable(Guid id)
        {
            var model = await _productDb.GetAsync<Product>(id);
            model.DataStatus = DataStatusEnum.Invalid;
            await _productDb.UpdateAsync<Product>(model);
            return true;
        }

        /// <summary>
        /// 获取产品
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns>产品</returns>
        public async Task<ProductInfo> FindById(Guid id)
        {
            var model = await _productDb.GetAsync<Product>(id);
            return model.ToViewModel<ProductInfo>();
        }
    }
}
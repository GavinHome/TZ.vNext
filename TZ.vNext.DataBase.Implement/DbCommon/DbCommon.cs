//-----------------------------------------------------------------------------------
// <copyright file="DbCommon.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/8 10:23:29</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model.Context;

namespace TZ.vNext.DataBase.Implement
{
    public class DbCommon : IDbCommon
    {
        private readonly AppDbContext _context;

        public DbCommon(AppDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public IQueryable<T> Get<T>() where T : class, IEntitySet
        {
            return _context.AsQueryable<T>();
        }

        public T Get<T>(object id) where T : class, IEntitySet
        {
            return Get<T>().FirstOrDefault(x => x.Id.ToString() == id.ToString());
        }

        public T Save<T>(T t) where T : class, IEntitySet
        {
            _context.Add(t);
            _context.SaveChanges();
            return t;
        }

        public bool Delete<T>(T t) where T : class, IEntitySet
        {
            _context.Remove<T>(t);
            return _context.SaveChanges() > 0;
        }

        public bool Delete<T>(object id) where T : class, IEntitySet
        {
            var entity = Get<T>(id);
            _context.Remove<T>(entity);
            return _context.SaveChanges() > 0;
        }

        public async Task<IQueryable<T>> GetAsync<T>() where T : class, IEntitySet
        {
            return await Task.Run(() => _context.AsQueryable<T>());
        }

        public async Task<T> GetAsync<T>(object id) where T : class, IEntitySet
        {
            return await Get<T>().FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
        }

        public async Task<T> SaveAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public T Update<T>(T t) where T : class, IEntitySet
        {
            _context.Update(t);
            _context.SaveChanges();
            return t;
        }

        public async Task<T> UpdateAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<bool> DeleteAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Remove(t);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync<T>(object id) where T : class, IEntitySet
        {
            var entity = Get<T>(id);
            _context.Remove<T>(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public T SaveOrUpdate<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var entity = Get<T>(t.Id);
            if (entity == null)
            {
                t = this.Save(t);
            }
            else
            {
                t = this.Update(t);
            }

            return t;
        }

        public async Task<T> SaveOrUpdateAsync<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var entity = await GetAsync<T>(t.Id);
            if (entity == null)
            {
                t = await this.SaveAsync(t);
            }
            else
            {
                t = await this.UpdateAsync(t);
            }

            return t;
        }

        public async Task<IList<T>> BatchSaveAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            _context.AddRange(colls);
            await _context.SaveChangesAsync();
            return colls;
        }

        public async Task<IList<T>> BatchUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            _context.UpdateRange(colls);
            await _context.SaveChangesAsync();
            return colls;
        }

        public async Task<IList<T>> BatchSaveOrUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            var query = from b in Get<T>() where colls.Contains(b.Id) select b.Id;

            var entities = await query.ToListAsync();

            var update = colls.Select(x => entities.Contains(x.Id)).ToList();

            var save = colls.Select(x => !entities.Contains(x.Id)).ToList();

            _context.AddRange(save);
            _context.UpdateRange(update);
            await _context.SaveChangesAsync();
            return colls;
        }

        public IList<T> BatchSave<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            _context.AddRange(colls);
            _context.SaveChanges();
            return colls;
        }

        public IList<T> BatchUpdate<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            _context.UpdateRange(colls);
            _context.SaveChanges();
            return colls;
        }

        public IList<T> BatchSaveOrUpdate<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            var query = from b in Get<T>() where colls.Contains(b.Id) select b.Id;

            var entities = query.ToList();

            var update = colls.Select(x => entities.Contains(x.Id)).ToList();

            var save = colls.Select(x => !entities.Contains(x.Id)).ToList();

            _context.AddRange(save);
            _context.UpdateRange(update);
            _context.SaveChanges();
            return colls;
        }

        public bool BatchDelete<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            _context.RemoveRange(colls);
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> BatchDeleteAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            _context.RemoveRange(colls);
            return await _context.SaveChangesAsync() > 0;
        }

        ///// <summary>
        ///// 使用 IQueryable 或者 Get() 应用到 ODataQueryOptions 并生成查询表达式
        ///// </summary>
        ///// <typeparam name="T">类型T</typeparam>
        ///// <param name="options">ODataQueryOptions 类型参数实例</param>
        ///// <param name="queryable">自定义的 IQueryable 类型实例, 如没有这个参数, 将使用 Get</param>
        ///// <returns>返回应用到 ODataQueryOptions 之后的 IQueryable</returns>
        ////protected IQueryable Get<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        ////{
        ////    queryable = queryable ?? Get<T>();
        ////    if (WebConfig.CustomQuery != null && EnableCustomQuery)
        ////    {
        ////        return options.ApplyTo(WebConfig.CustomQuery.Invoke(queryable));
        ////    }

        ////    return options.ApplyTo(queryable);
        ////}

        ////protected async Task<PageResult<T>> GetToPageResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        ////{
        ////    var query = Get<T>(options, queryable) as IQueryable<T>;
        ////    ////return new PageResult<object>(await query.ToListAsync(), null, Request.ODataProperties().TotalCount ?? 0);
        ////    var result = await query.ToListAsync<T>();
        ////    return new PageResult<T>(result, null, result.Count);
        ////}

        ////protected async Task<IQueryable> GetPageResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        ////{
        ////    ////var appliedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Filter | AllowedQueryOptions.Expand;
        ////    ////return options.ApplyTo(await GetAsync<T>(), appliedQueryOptions);
        ////    return options.ApplyTo(await GetAsync<T>());
        ////}
    }
}

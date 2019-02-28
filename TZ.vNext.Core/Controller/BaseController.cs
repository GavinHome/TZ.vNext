//-----------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="天职工程咨询股份有限公司版权所有">
//     Copyright  TZEPM. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Extensions;
using TZ.vNext.Core.Utility;

namespace TZ.vNext.Core
{
    public class BaseController : Controller
    {
        private readonly DbContext _context;

        public BaseController(DbContext context)
        {
            _context = context;
            EnableCustomQuery = true;
            WebConfig.DbContext = context;
        }

        /// <summary>
        /// 是否启用CustomQuery的统一底层过滤查询
        /// </summary>
        protected bool EnableCustomQuery { get; set; }

        #region base api

        /// <summary>
        /// 底层通用查询方法, 如果cachePriority参数值为true, 并且类型 T 被 Cacheable 特性标记, 则会优先从缓存获取数据, 而不是数据库
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <returns>返回未断开的查询</returns>
        protected IQueryable<T> Get<T>() where T : class, IEntitySet
        {
            return _context.AsQueryable<T>();
        }

        /// <summary>
        /// 使用 IQueryable 或者 Get() 应用到 ODataQueryOptions 并生成查询表达式
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="options">ODataQueryOptions 类型参数实例</param>
        /// <param name="queryable">自定义的 IQueryable 类型实例, 如没有这个参数, 将使用 Get</param>
        /// <returns>返回应用到 ODataQueryOptions 之后的 IQueryable</returns>
        protected IQueryable Get<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        {
            GuardUtils.NotNull(options, nameof(options));
            queryable = queryable ?? Get<T>();
            if (WebConfig.CustomQuery != null && EnableCustomQuery)
            {
                return options.ApplyTo(WebConfig.CustomQuery.Invoke(queryable));
            }

            return options.ApplyTo(queryable);
        }

        protected T Get<T>(object id) where T : class, IEntitySet
        {
            return Get<T>().FirstOrDefault(x => x.Id == id);
        }

        protected T Save<T>(T t) where T : class, IEntitySet
        {
            _context.Add(t);
            _context.SaveChanges();
            return t;
        }

        protected bool Delete<T>(T t) where T : class, IEntitySet
        {
            _context.Remove<T>(t);
            return _context.SaveChanges() > 0;
        }

        protected bool Delete<T>(object id) where T : class, IEntitySet
        {
            var entity = Get<T>(id);
            _context.Remove<T>(entity);
            return _context.SaveChanges() > 0;
        }

        protected async Task<IQueryable<T>> GetAsync<T>() where T : class, IEntitySet
        {
            return await Task.Run(() => _context.AsQueryable<T>());
            ////return _context.AsQueryable<T>();
        }

        protected async Task<T> GetAsync<T>(object id) where T : class, IEntitySet
        {
            return await Get<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        protected async Task<T> SaveAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        protected T Update<T>(T t) where T : class, IEntitySet
        {
            _context.Update(t);
            _context.SaveChanges();
            return t;
        }

        protected async Task<T> UpdateAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
            return t;
        }

        protected async Task<bool> DeleteAsync<T>(T t) where T : class, IEntitySet
        {
            _context.Remove(t);
            return await _context.SaveChangesAsync() > 0;
        }

        protected async Task<bool> DeleteAsync<T>(object id) where T : class, IEntitySet
        {
            var entity = Get<T>(id);
            _context.Remove<T>(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        protected async Task<PageResult<T>> GetToPageResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        {
            var query = Get<T>(options, queryable) as IQueryable<T>;
            ////return new PageResult<object>(await query.ToListAsync(), null, Request.ODataProperties().TotalCount ?? 0);
            var result = await query.ToListAsync<T>();
            return new PageResult<T>(result, null, result.Count);
        }

        protected async Task<IQueryable> GetPageResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        {
            GuardUtils.NotNull(options, nameof(options));
            ////var appliedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Filter | AllowedQueryOptions.Expand;
            ////return options.ApplyTo(await GetAsync<T>(), appliedQueryOptions);
            return options.ApplyTo(await GetAsync<T>());
        }

        #endregion

        #region to IActionResult
        protected IActionResult ToActionResult<T>(T t)
        {
            return Ok(t.ToJsonResult<T>());
        }

        protected IActionResult ToActionResult(IActionResult t)
        {
            return Ok(t);
        }

        protected IActionResult GetToActionResult<T>(object id) where T : class, IEntitySet
        {
            return Ok(Get<T>(id));
        }

        protected IActionResult SaveToActionResult<T>(T t) where T : class, IEntitySet
        {
            return Ok(Save(t));
        }

        protected IActionResult DeleteToActionResult<T>(T t) where T : class, IEntitySet
        {
            return Ok(Delete(t));
        }

        protected IActionResult DeleteToActionResult<T>(object id) where T : class, IEntitySet
        {
            return Ok(Delete<T>(id));
        }

        protected async Task<IActionResult> GetToActionResultAsync<T>(ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        {
            return Ok(await GetToPageResultAsync<T>(options, queryable));
        }

        protected async Task<IActionResult> GetToActionResultAsync<T>(object id) where T : class, IEntitySet
        {
            return Ok(await GetAsync<T>(id));
        }

        protected async Task<IActionResult> SaveToActionResultAsync<T>(T t) where T : class, IEntitySet
        {
            return Ok(await SaveAsync(t));
        }

        protected async Task<IActionResult> DeleteToActionResultAsync<T>(T t) where T : class, IEntitySet
        {
            return Ok(await DeleteAsync(t));
        }

        protected async Task<IActionResult> DeleteToActionResultAsync<T>(object id) where T : class, IEntitySet
        {
            return Ok(await DeleteAsync<T>(id));
        }
        #endregion

        ////#region to ActionResult
        ////protected ActionResult<T> GetToActionResult<T>(object id) where T : class, IEntitySet
        ////{
        ////    return Get<T>(id).ToActionResult();
        ////}

        ////protected ActionResult<T> SaveToActionResult<T>(T t) where T : class, IEntitySet
        ////{
        ////    return Save(t).ToActionResult();
        ////}

        ////protected ActionResult<bool> DeleteToActionResult<T>(T t) where T : class, IEntitySet
        ////{
        ////    return Delete(t).ToActionResult();
        ////}

        ////protected ActionResult<bool> DeleteToActionResult<T>(object id) where T : class, IEntitySet
        ////{
        ////         return Delete<T>(id).ToActionResult();
        ////}

        ////protected async Task<ActionResult<T>> GetToActionResultAsync<T>(object id) where T : class, IEntitySet
        ////{
        ////    return (await GetAsync<T>(id)).ToActionResult();
        ////}

        ////protected async Task<ActionResult<T>> SaveToActionResultAsync<T>(T t) where T : class, IEntitySet
        ////{
        ////    return (await SaveAsync(t)).ToActionResult();
        ////}

        ////protected async Task<ActionResult<bool>> DeleteToActionResultAsync<T>(T t) where T : class, IEntitySet
        ////{
        ////    return (await DeleteAsync(t)).ToActionResult();
        ////}

        ////protected async Task<ActionResult<bool>> DeleteToActionResultAsync<T>(object id) where T : class, IEntitySet
        ////{
        ////    return (await DeleteAsync<T>(id)).ToActionResult();
        ////}
        ////#endregion
        
        ////#region 其他基础类 api

        ///// <summary>
        ///// 处理导航引用数据
        ///// </summary>
        ///// <typeparam name="T">类型参数</typeparam>
        ///// <param name="t">类型的运行时实例</param>
        ///// <returns>结果</returns>
        ////protected async Task ProcessNavigationData<T>(T t) where T : class, IEntitySet
        ////{
        ////    t.ProcessNavigationArguments();
        ////    await _context.RemoveOldNavigationData(t);
        ////    _context.AddNewNavigationData(t);
        ////}

        ///// <summary>
        ///// 初始化方法, 重载基类方法用以支持非 httpget 类型的 odata 参数
        ///// </summary>
        ///// <param name="requestContext">requestContext</param>
        ////protected override void Initialize(RequestContext requestContext)
        ////{
        ////    base.Initialize(requestContext);
        ////    Request.GetQueryOptions();
        ////}

        ///// <summary>
        ///// 释放资源的方法, 重载基类的方法释放资源
        ///// </summary>
        ///// <param name="disposing">bool</param>
        ////protected override void Dispose(bool disposing)
        ////{
        ////    _context.Dispose();
        ////    base.Dispose(disposing);
        ////}
        ////#endregion
    }
}

//-----------------------------------------------------------------------
// <copyright file="DbContextExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2017/3/22 12:52:35</date>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using TZ.vNext.Core.Attributes;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Enum;

namespace TZ.vNext.Core.Extensions
{
    /// <summary>
    /// dbcontext extension class
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 统一数据查询入口, 可缓存的数据源
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="ctx">数据库上下文</param>
        /// <returns>返回对当前类型的数据库上下文的引用</returns>
        public static IQueryable<T> AsQueryable<T>(this DbContext ctx) where T : class, IEntitySet
        {
            if (typeof(T).IsCached() && WebConfig.EnableCache)
            {
                string cacheKey = typeof(T).GetCacheKey();
                IEnumerable<T> entitySets = HttpCache.Current.Get<IEnumerable<T>>(cacheKey);
                if (entitySets == null)
                {
                    entitySets = ctx.Set<T>().ToList();
                    HttpCache.Current.Insert(cacheKey, entitySets);
                }

                return entitySets.AsQueryable();
            }
            else
            {
                return ctx.Set<T>().AsQueryable();
            }
        }

        /// <summary>
        /// 统一数据查询入口, 可缓存的数据源
        /// </summary>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="type">对于数据库上下文要查询的数据类型</param>
        /// <returns>返回非类型化的数据库上下文的引用</returns>
        public static IQueryable AsQueryable(this DbContext ctx, Type type)
        {
            try
            {
                var methods = typeof(DbContextExtensions).GetTypeInfo().GetDeclaredMethods(nameof(AsQueryable));
                if (methods.IsNotNullOrEmpty())
                {
                    var method = methods.FirstOrDefault(x => x.IsGenericMethod);
                    if (method != null)
                    {
                        return method.MakeGenericMethod(type).Invoke(ctx, new object[] { ctx }) as IQueryable;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 底层通用查询方法, 如果cachePriority参数值为true, 并且类型 T 被 Cacheable 特性标记, 则会优先从缓存获取数据, 而不是数据库
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="ctx">数据库上下文</param>
        /// <returns>返回未断开的查询</returns>
        public static IQueryable<T> Get<T>(this DbContext ctx) where T : class, IEntitySet
        {
            return ctx.AsQueryable<T>();
        }

        /// <summary>
        /// 通用查询方法, 使用 id 获取当前对象
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="id">数据主键</param>
        /// <returns>返回符合当前主键的数据</returns>
        public static T Get<T>(this DbContext ctx, object id) where T : class, IEntitySet
        {
            return ctx.Get<T>().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 通用查询方法, 使用 id 获取当前对象的异步重载
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="id">数据主键</param>
        /// <returns>返回符合当前主键的数据</returns>
        public static async Task<T> GetAsync<T>(this DbContext ctx, object id) where T : class, IEntitySet
        {
            return await ctx.Get<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 使用 IQueryable 或者 Get() 应用到 ODataQueryOptions 并生成查询表达式
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="options">ODataQueryOptions 类型参数实例</param>
        /// <param name="queryable">自定义的 IQueryable 类型实例, 如没有这个参数, 将使用 Get</param>
        /// <returns>返回应用到 ODataQueryOptions 之后的 IQueryable</returns>
        public static IQueryable Get<T>(this ODataQueryOptions<T> options, IQueryable<T> queryable = null) where T : class, IEntitySet
        {
            queryable = queryable ?? WebConfig.DbContext.Get<T>();
            if (WebConfig.CustomQuery != null)
            {
                return options.ApplyTo(WebConfig.CustomQuery(queryable));
            }

            return options.ApplyTo(queryable);
        }

        /// <summary>
        /// set CreatedOn, CreatedBy and ModifiedBy field value to this generic type entity, and attached and added it to current dbcontext
        /// </summary>
        /// <typeparam name="T">IEntitySet</typeparam>
        /// <param name="ctx">dbcontext instance</param>
        /// <param name="t">IEntitySet instance</param>
        public static void Save<T>(this DbContext ctx, T t) where T : class, IEntitySet
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException(typeof(T).FullName);
                }

                var isNew = t.IsNullOrEmpty();
                if (isNew && t is EntitySet)
                {
                    (t as EntitySet).Id = WebConfig.NewGuid();
                }

                if (t is EntitySetWithCreate)
                {
                    ////var user = UserConext.Current?.Code;
                    if (isNew)
                    {
                        var createEntitySet = t as EntitySetWithCreate;
                        createEntitySet.CreateAt = DateTime.Now;
                        ////createEntitySet.CreateBy = createEntitySet.CreateBy ?? user;
                    }

                    if (t is EntitySetWithCreateAndUpdate)
                    {
                        var updateEntitySet = t as EntitySetWithCreateAndUpdate;
                        updateEntitySet.UpdateAt = DateTime.Now;
                        ////updateEntitySet.UpdateBy = updateEntitySet.UpdateBy ?? user;
                    }

                    if (t is EntitySetWithAllStatus)
                    {
                        var statusEntitySet = t as EntitySetWithAllStatus;

                        ////if (isNew)
                        ////{
                        ////    statusEntitySet.CreateByOrganizationId = UserContextInfo.Current?.LoginInfo?.OrganizationId;
                        ////}
                        if (statusEntitySet.ApprovalStatus == ApproveStatusEnum.Auditing)
                        {
                            statusEntitySet.SubmitAt = DateTime.Now;
                        }
                    }
                }

                if (ctx.Get<T>(t.Id) == null)
                {
                    ctx.Set<T>().Add(t);
                }
                else
                {
                    ctx.Set<T>().Update(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// set CreatedOn, CreatedBy and ModifiedBy field value to this generic type entity, and attached and added it to current dbcontext
        /// </summary>
        /// <param name="ctx">dbcontext instance</param>
        /// <param name="obj">IEntitySet instance</param>
        public static void Save(this DbContext ctx, object obj)
        {
            try
            {
                var methods = typeof(DbContextExtensions).GetTypeInfo().GetDeclaredMethods(nameof(Save));
                if (methods.IsNotNullOrEmpty())
                {
                    var method = methods.FirstOrDefault(x => x.IsGenericMethod);
                    if (method != null)
                    {
                        var type = obj.GetType();
                        method.MakeGenericMethod(type).Invoke(ctx, new object[] { ctx, obj });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// attached and deleted this generic type entity to current dbcontext
        /// </summary>
        /// <typeparam name="T">IEntitySet</typeparam>
        /// <param name="ctx">dbcontext instance</param>
        /// <param name="t">IEntitySet instance</param>
        /// <param name="operationType">操作类型, 这里只接受是否逻辑删除</param>
        public static void Delete<T>(this DbContext ctx, T t, OperationTypeEnum operationType = OperationTypeEnum.Delete) where T : class, IEntitySet
        {
            try
            {
                if (t.IsNullOrEmpty())
                {
                    throw new ArgumentNullException(typeof(T).FullName);
                }

                ctx.Delete<T>(t.Id, operationType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// attached and deleted this generic type entity to current dbcontext
        /// </summary>
        /// <typeparam name="T">IEntitySet</typeparam>
        /// <param name="ctx">dbcontext instance</param>
        /// <param name="id">IEntitySet instance id</param>
        /// <param name="operationType">操作类型, 这里只接受是否逻辑删除</param>
        public static void Delete<T>(this DbContext ctx, object id, OperationTypeEnum operationType = OperationTypeEnum.Delete) where T : class, IEntitySet
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(typeof(T).FullName);
                }

                T t = ctx.Set<T>().Find(id);
                if (t == null)
                {
                    throw new ArgumentException(typeof(T).FullName + $" not found with Id '{id}'");
                }

                if (operationType == OperationTypeEnum.LogicDelete)
                {
                    (t as EntitySetWithCreate).DataStatus = DataStatusEnum.Deleted;
                    ctx.Set<T>().Update(t);
                }
                else
                {
                    ctx.Set<T>().Remove(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// attached and deleted this generic type entity to current dbcontext
        /// </summary>
        /// <param name="ctx">dbcontext instance</param>
        /// <param name="obj">IEntitySet instance</param>
        /// <param name="operationType">操作类型, 这里只接受是否逻辑删除</param>
        public static void Delete(this DbContext ctx, object obj, OperationTypeEnum operationType = OperationTypeEnum.Delete)
        {
            try
            {
                var t = obj as IEntitySet;
                if (t.IsNullOrEmpty())
                {
                    throw new ArgumentNullException(nameof(obj));
                }

                if (operationType == OperationTypeEnum.LogicDelete)
                {
                    (t as EntitySetWithCreate).DataStatus = DataStatusEnum.Deleted;
                    ctx.Save(t);
                }
                else
                {
                    ctx.Remove(obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 自动处理对象的子级旧数据引用
        /// 主记录为 RecordableAttribute 特性标记的类型, 否则被集合引用的类型将视为子记录
        /// </summary>
        /// <typeparam name="T">类型参数T</typeparam>
        /// <param name="ctx">上下文</param>
        /// <param name="t">类型实例</param>
        /// <returns>无返回, 请保持上下文的连接, 让事务得以继续执行</returns>
        public static async Task RemoveOldNavigationData<T>(this DbContext ctx, T t) where T : class, IEntitySet
        {
            if (t.IsNotNullOrEmpty())
            {
                //此处目前只处理集合引用
                var properties = typeof(T).GetProperties().Where(x => x.PropertyType.IsFromType(typeof(IEnumerable<IEntitySet>))).ToList();
                if (properties.IsNotNullOrEmpty())
                {
                    foreach (var prop in properties)
                    {
                        var values = prop.GetValue(t) as IEnumerable<IEntitySet>;
                        Type type = prop.PropertyType.GetGenericType();
                        if (type.GetCustomAttribute<RecordableAttribute>() == null)
                        {
                            var foreignKey = type.GetForeignKey<T>();
                            var queryable = ctx.Set<T>().Where(x => x.Id.ToString() == foreignKey);
                            if (values.IsNotNullOrEmpty())
                            {
                                //TBC, 此处要验证下 x 在 lamdba 表达式中是否可以执行此方法
                                var ids = values.Where(x => x.IsNotNullOrEmpty()).Select(x => x.Id);
                                queryable = queryable.Where(x => ids.Contains(x.Id));
                            }

                            var list = await queryable.ToListAsync();
                            if (list.IsNotNullOrEmpty())
                            {
                                foreach (var item in list)
                                {
                                    //删除的前提是这条数据不能在使用中
                                    ctx.Delete(item, OperationTypeEnum.Delete);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 自动处理对象子级的新数据引用关系
        /// </summary>
        /// <typeparam name="T">类型参数T</typeparam>
        /// <param name="ctx">上下文</param>
        /// <param name="t">类型实例</param>
        public static void AddNewNavigationData<T>(this DbContext ctx, T t) where T : class, IEntitySet
        {
            ctx.Save(t);

            var properties = typeof(T).GetProperties().Where(x => x.PropertyType.IsFromType(typeof(IEnumerable<IEntitySet>))).ToList();
            if (properties.IsNotNullOrEmpty())
            {
                foreach (var prop in properties)
                {
                    var values = prop.GetValue(t) as IEnumerable<object>;
                    if (values.IsNotNullOrEmpty())
                    {
                        var type = values.FirstOrDefault().GetType();
                        var foreignKey = type.GetForeignKey<T>();
                        var property = type.GetProperty(foreignKey);
                        if (property != null)
                        {
                            foreach (var value in values)
                            {
                                ctx.AddNewAttachmentData(value);
                                property.SetValue(value, t.Id);
                                ctx.Save(value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 自动处理附件, 处理对象为包含附件外键的数据模型, 比如, 处理客户沟通的附件, 则参数对象 obj 应为 CustomerCommunicateAttachment 类型对象
        /// </summary>
        /// <param name="ctx">上下文</param>
        /// <param name="obj">类型实例</param>
        public static void AddNewAttachmentData(this DbContext ctx, object obj)
        {
            var type = obj.GetType();
            var foreignKey = type.GetForeignKey(WebConfig.AttachmentEntitySetType);
            if (foreignKey.IsNotNullOrEmpty())
            {
                var property = type.GetProperty(foreignKey);
                if (property != null)
                {
                    var value = property.GetValue(obj);
                    if ((value as IEntitySet).IsNullOrEmpty())
                    {
                        var fileName = obj.GetPropertyValue(nameof(EntitySetWithAttachment.FileName));
                        if (fileName != null)
                        {
                            var att = WebConfig.GetAttachmentFromSession(fileName.ToString());
                            if (att != null)
                            {
                                ctx.Save(att);
                                property.SetValue(obj, (att as IEntitySet).Id);
                            }
                        }
                    }
                }
            }
        }
    }
}

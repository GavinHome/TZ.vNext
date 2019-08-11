//-----------------------------------------------------------------------------------
// <copyright file="IDbCommon.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/8 10:21:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TZ.vNext.Core.Entity;

namespace TZ.vNext.Database.Contracts
{
    public interface IDbCommon
    {
        IQueryable<T> Get<T>() where T : class, IEntitySet;

        T Get<T>(object id) where T : class, IEntitySet;

        T Save<T>(T t) where T : class, IEntitySet;

        T Update<T>(T t) where T : class, IEntitySet;

        T SaveOrUpdate<T>(T t) where T : class, IEntitySet;

        bool Delete<T>(T t) where T : class, IEntitySet;

        bool Delete<T>(object id) where T : class, IEntitySet;

        IList<T> BatchSave<T>(IList<T> colls) where T : class, IEntitySet;

        IList<T> BatchUpdate<T>(IList<T> colls) where T : class, IEntitySet;

        IList<T> BatchSaveOrUpdate<T>(IList<T> colls) where T : class, IEntitySet;

        bool BatchDelete<T>(IList<T> colls) where T : class, IEntitySet;

        Task<IQueryable<T>> GetAsync<T>() where T : class, IEntitySet;

        Task<T> GetAsync<T>(object id) where T : class, IEntitySet;

        Task<T> SaveAsync<T>(T t) where T : class, IEntitySet;        

        Task<T> UpdateAsync<T>(T t) where T : class, IEntitySet;

        Task<T> SaveOrUpdateAsync<T>(T t) where T : class, IEntitySet;

        Task<bool> DeleteAsync<T>(T t) where T : class, IEntitySet;

        Task<bool> DeleteAsync<T>(object id) where T : class, IEntitySet;       

        Task<IList<T>> BatchSaveAsync<T>(IList<T> colls) where T : class, IEntitySet;

        Task<IList<T>> BatchUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet;

        Task<IList<T>> BatchSaveOrUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet;

        Task<bool> BatchDeleteAsync<T>(IList<T> colls) where T : class, IEntitySet;
    }
}

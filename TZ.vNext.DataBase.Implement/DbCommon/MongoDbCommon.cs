//-----------------------------------------------------------------------------------
// <copyright file="MongoDbCommon.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/8 10:23:29</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TZ.vNext.Core.Entity;
using TZ.vNext.Core.Utility;
using TZ.vNext.Database.Contracts;
using TZ.vNext.Model.Context;

namespace TZ.vNext.DataBase.Implement
{
    /// <summary>
    /// Mongo Db Common
    /// </summary>
    public class MongoDbCommon : IMongoDbCommon
    {
        private readonly MongoContext _context;

        public MongoDbCommon(MongoContext dbcontext)
        {
            _context = dbcontext;
        }

        public IQueryable<T> Get<T>() where T : class, IEntitySet
        {
            return _context.Of<T>().AsQueryable<T>();
        }

        public T Get<T>(object id) where T : class, IEntitySet
        {
            return this.Get<T>().FirstOrDefault(x => x.Id == id);
        }

        public T Save<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            _context.Of<T>().ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });

            return t;
        }

        public bool Delete<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            return this.Delete<T>(t.Id);
        }

        public bool Delete<T>(object id) where T : class, IEntitySet
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            _context.Of<T>().DeleteOne(filter);
            return Get<T>(id) == null;
        }

        public async Task<IQueryable<T>> GetAsync<T>() where T : class, IEntitySet
        {
            return (await _context.OfAsync<T>()).AsQueryable<T>();
        }

        public async Task<T> GetAsync<T>(object id) where T : class, IEntitySet
        {
            return await _context.Of<T>().AsQueryable<T>().FirstOrDefaultAsync<T>(x => x.Id == id);
        }

        public async Task<T> SaveAsync<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            await _context.Of<T>().ReplaceOneAsync(filter, t, new UpdateOptions() { IsUpsert = true });
            return t;
        }

        public T Update<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            _context.Of<T>().ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = false });
            return t;
        }

        public async Task<T> UpdateAsync<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            await _context.Of<T>().ReplaceOneAsync(filter, t, new UpdateOptions() { IsUpsert = false });
            return t;
        }

        public async Task<bool> DeleteAsync<T>(T t) where T : class, IEntitySet
        {
            GuardUtils.NotNull(t, nameof(t));
            var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            await _context.Of<T>().DeleteOneAsync(filter);
            return await GetAsync<T>(t.Id) == null;
        }

        public async Task<bool> DeleteAsync<T>(object id) where T : class, IEntitySet
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            await _context.Of<T>().DeleteOneAsync(filter);
            return await GetAsync<T>(id) == null;
        }

        public T Add<T>(T model)
        {
            _context.Of<T>().InsertOne(model);
            return model;
        }

        public string NewId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        public T SaveOrUpdate<T>(T t) where T : class, IEntitySet
        {
            ////var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            ////_context.Of<T>().ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });
            this.Save<T>(t);
            return t;
        }

        public async Task<T> SaveOrUpdateAsync<T>(T t) where T : class, IEntitySet
        {
            ////var filter = Builders<T>.Filter.Eq(x => x.Id, t.Id);
            ////await _context.Of<T>().ReplaceOneAsync(filter, t, new UpdateOptions() { IsUpsert = true });
            await this.SaveAsync<T>(t);
            return t;
        }

        public async Task<IList<T>> BatchSaveAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                await _context.Of<T>().InsertManyAsync(colls, new InsertManyOptions() { IsOrdered = true });
            }

            return colls;
        }

        public async Task<IList<T>> BatchUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                foreach (var item in colls)
                {
                    await this.UpdateAsync(item);
                }
                ////var filter = Builders<T>.Filter.In(x => x.Id, colls.Select(x => x.Id));
                ////var update = Builders<T>.Update.Mul<T>(filter, colls);
                ////var update = Builders<T>.Update.AddToSet(x=>x., colls);
                ////await _context.Of<T>().UpdateManyAsync(filter, new , new UpdateOptions() { IsUpsert = true });
            }

            return colls;
        }

        public async Task<IList<T>> BatchSaveOrUpdateAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                foreach (var item in colls)
                {
                    await this.SaveOrUpdateAsync(item);
                }
            }

            return colls;
        }

        public IList<T> BatchSave<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                _context.Of<T>().InsertMany(colls, new InsertManyOptions() { IsOrdered = true });
            }

            return colls;
        }

        public IList<T> BatchUpdate<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                foreach (var item in colls)
                {
                    this.Update(item);
                }
            }

            return colls;
        }

        public IList<T> BatchSaveOrUpdate<T>(IList<T> colls) where T : class, IEntitySet
        {
            if (colls != null && colls.Count > 0)
            {
                foreach (var item in colls)
                {
                    this.SaveOrUpdate(item);
                }
            }

            return colls;
        }

        public bool BatchDelete<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            var filter = Builders<T>.Filter.In(x => x.Id, colls.Select(x => x.Id));
            return _context.Of<T>().DeleteMany(filter).DeletedCount > 0;
        }

        public async Task<bool> BatchDeleteAsync<T>(IList<T> colls) where T : class, IEntitySet
        {
            GuardUtils.NotNull(colls, nameof(colls));
            long result = 0;
            if (colls.Any())
            {
                var filter = Builders<T>.Filter.In(x => x.Id, colls.Select(x => x.Id));
                var deleteResult = await _context.Of<T>().DeleteManyAsync(filter);
                result = deleteResult.DeletedCount;
            }
           
            return result > 0;
        }
    }
}

//-----------------------------------------------------------------------------------
// <copyright file="MongoDbServiceCollectionExtensions.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2019/09/02 15:04:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;
using TZ.vNext.Core.Mongo;
using TZ.vNext.Core.Mongo.Context;
using TZ.vNext.Model.Context;

namespace TZ.vNext.Web.Extensions.Mongo
{
    public static class MongoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(this IServiceCollection serviceCollection, Action<MongoDbContextOptionsBuilder> optionsAction) where TContext : MongoContext
        {
            var options = new MongoDbContextOptionsBuilder();
            optionsAction(options);
            serviceCollection.AddSingleton<MongoDbContextOptions>(options.Options);
            serviceCollection.AddSingleton<MongoContext, MongoDbContext>();
            return serviceCollection;
        }
    }
}

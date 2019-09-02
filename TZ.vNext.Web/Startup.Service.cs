//-----------------------------------------------------------------------------------
// <copyright file="Startup.Service.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TZ.vNext.Core.Cache;
using TZ.vNext.Database.Contracts;
using TZ.vNext.DataBase.Implement;
using TZ.vNext.Services.Contracts;
using TZ.vNext.Services.Implement;
using TZ.vNext.Web.Filters;
using TZ.vNext.Web.PermissionExtensions;

namespace TZ.vNext.Web
{
    public static class StartupService
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            ////Cache
            services.AddSingleton<ICache, CustomCache>();

            ////EF & Mongo Services
            ConfigureDbService(services);

            ////Service Service
            ConfigureDomainService(services);

            ////Authorization Handler & Filters
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<AsyncActionFilter>();
        }

        private static void ConfigureDbService(IServiceCollection services)
        {
            var dbContracts = System.Reflection.Assembly.GetAssembly(typeof(IDbCommon)).GetTypes().Where(x => x.IsInterface && (x.GetInterfaces().Contains(typeof(IDbCommon)) || x.FullName == typeof(IDbCommon).FullName)).ToList();
            var dbImplements = System.Reflection.Assembly.GetAssembly(typeof(DbCommon)).GetTypes().Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(IDbCommon))).ToList();
            foreach (var contract in dbContracts)
            {
                var implement = dbImplements.FirstOrDefault(x => x.GetInterfaces().Contains(contract) && x.FullName == $"{typeof(DbCommon).Namespace}.{contract.Name.Remove(0, 1)}");

                if (implement == null)
                {
                    throw new NotImplementedException(contract.FullName + " Not Implemented");
                }

                services.AddScoped(contract, implement);
            }
        }

        private static void ConfigureDomainService(IServiceCollection services)
        {
            var servContracts = System.Reflection.Assembly.GetAssembly(typeof(ISalaryService)).GetTypes().Where(x => x.IsInterface && x.FullName.EndsWith("Service")).ToList();
            var servImplements = System.Reflection.Assembly.GetAssembly(typeof(SalaryService)).GetTypes().Where(x => x.IsClass && x.FullName.EndsWith("Service")).ToList();
            foreach (var contract in servContracts)
            {
                var implement = servImplements.FirstOrDefault(x => x.GetInterfaces().Contains(contract) && x.FullName == $"{typeof(SalaryService).Namespace}.{contract.Name.Remove(0, 1)}");

                if (implement == null)
                {
                    throw new NotImplementedException(contract.FullName + " Not Implemented");
                }

                if (contract.FullName == typeof(IAuthFunctionService).FullName)
                {
                    services.AddSingleton(contract, implement);
                }
                else
                {
                    services.AddScoped(contract, implement);
                }
            }
        }
    }
}

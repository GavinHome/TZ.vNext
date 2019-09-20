//-----------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="TZ.vNext">
//     Copyright  TZ.vNext. All rights reserved.
// </copyright>
// <author>tzxx</author>
// <date>2018/11/6 8:13:46</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TZ.vNext.Core;
using TZ.vNext.Core.Cache;
using TZ.vNext.Core.Const;
using TZ.vNext.Model.Context;
using TZ.vNext.Web.Extensions.Mongo;
using TZ.vNext.Web.Filters;
using TZ.vNext.Web.Middleware;
using TZ.vNext.Web.MiddlewareExtensions;

namespace TZ.vNext.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    "image/svg+xml",
                    "application/x-javascript",
                    "text/javascript",
                    "image/jpeg",
                    "image/gif",
                    "image/png",
                    "image/jpg",
                    "font/x-woff",
                    "application/font-woff2",
                    "application/octet-stream",
                    "image/x-icon",
                    "application/vnd.ms-fontobject",
                };
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.UseRowNumberForPaging());
            });

            ////services.AddODataQueryFilter();
            services.AddOData();

            ////services.AddSingleton<MongoContext, MongoDbContext>();

            services.AddMongoDbContext<MongoDbContext>(options =>
            {
                options.UseMongoServer(Configuration.GetConnectionString("MongodbConnection"));
            });

            ////Maintain property names during serialization. See:https://github.com/aspnet/Announcements/issues/194
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new KendoUiExtensions.CustomeModelBinderProvider());
                options.Filters.AddService<AsyncActionFilter>();

                foreach (var outputFormatter in options.OutputFormatters.OfType<Microsoft.AspNet.OData.Formatter.ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<Microsoft.AspNet.OData.Formatter.ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                ////options.Filters.Add<HttpGlobalExceptionFilter>(); //加入全局异常类
            });

            services.AddKendo();

            ////add business services
            services.ConfigureBusinessServices();

            ////add auth services
            ConfigureAuth(services);

            ////Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "TZIWB API", Version = "v1" });
            });
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });

                ////Profiler for Entity Framework, need install the package EntityFrameworkProfiler
                ////HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                ////HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.InitializeForProduction(33267, "TZ.vNext");
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseStaticFiles();

            app.UseTokenProvider(GetTokenProviderOptions());

            app.UseAuthentication();

            ////Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            ////Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TZ.vNext API V1");
            });

            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().MaxTop(null).Count();

                builder.MapODataServiceRoute("ODataRoute", "odata", AppDbContext.GetEdmModel(app.ApplicationServices));

                builder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                builder.MapRoute("defaultApi", "api/{controller}/{action}/{id?}");

                builder.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });

                //// uncomment the following line to Work-around for #1175 in beta1
                builder.EnableDependencyInjection();
            });

            ////app.MapWhen(x => x.Request.Path.Value.StartsWith("/app"), builder =>
            ////{
            ////    builder.UseMvc(routes =>
            ////    {
            ////        routes.MapSpaFallbackRoute(
            ////            name: "spa-fallback",
            ////            defaults: new { controller = "Home", action = "Index" });
            ////    });
            ////});

            ////app.MapWhen(x => !x.Request.Path.Value.StartsWith("/web"), builder =>
            ////{
            ////    builder.UseMvc(routes =>
            ////    {
            ////        routes.MapSpaFallbackRoute(
            ////            name: "spa-fallback",
            ////            defaults: new { controller = "Home", action = "Index" });
            ////    });
            ////});

            ////app.UseKendo(env);

            RegisterGlobalServices(app.ApplicationServices);

            RegisterGlobalConfig();
        }

        private void RegisterGlobalConfig()
        {
            SystemVariableConst.Redis_Switch = bool.Parse(Configuration.GetSection("TZIWB:Redis:Switch").Value);
            SystemVariableConst.Redis_ConnectionString = Configuration.GetSection("TZIWB:Redis:ConnectionString").Value;
            SystemVariableConst.Redis_DefaultKey = Configuration.GetSection("TZIWB:Redis:DefaultKey").Value;

            SystemVariableConst.Dingding_AgentId = Configuration.GetSection("TZIWB:Dingding:BaseUrl").Value;
            SystemVariableConst.Dingding_CorpId = Configuration.GetSection("TZIWB:Dingding:DingdingCorpId").Value;
            SystemVariableConst.Dingding_CorpSecret = Configuration.GetSection("TZIWB:Dingding:DingdingCorpSecret").Value;
            SystemVariableConst.Dingding_AccessTokenExpireTime = Configuration.GetSection("TZIWB:Dingding:DingdingAccessTokenExpireTime").Value;
            SystemVariableConst.Dingding_JsTicketExpireTime = Configuration.GetSection("TZIWB:Dingding:DingdingJsTicketExpireTime").Value;

            ////SystemVariableConst.TemplateFilesPath = System.IO.Directory.GetCurrentDirectory() + "/TemplateFiles";
            ////SystemVariableConst.StaticTemplateMap = RegisterStaticTemplateFiles();
        }

        private System.Collections.Generic.IDictionary<string, string> RegisterStaticTemplateFiles()
        {
            var map = new System.Collections.Generic.Dictionary<string, string>();
            map.Add("五险一金模板", "五险一金模板.xlsx");
            map.Add("薪酬导入项模板", "薪酬导入项模板.xlsx");
            return map;
        }

        private void RegisterGlobalServices(IServiceProvider servicePrivider)
        {
            HttpCache.SetContext(servicePrivider.GetService<ICache>());
        }
    }
}

﻿using Destiny.Core.Flow.AspNetCore.Mvc.Filters;
using Destiny.Core.Flow.AutoMapper;
using Destiny.Core.Flow.Caching.CSRedis;
using Destiny.Core.Flow.Dependency;
using Destiny.Core.Flow.Events;
using Destiny.Core.Flow.Extensions;
using Destiny.Core.Flow.Modules;
using Destiny.Core.Flow.Options;
using Destiny.Core.Flow.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Destiny.Core.Flow.API.Startups
{
    [DependsOn(typeof(DependencyAppModule), 
               typeof(SwaggerModule),
               typeof(IdentityModule),
               typeof(FunctionModule),
               typeof(EventBusAppModule),
               typeof(EntityFrameworkCoreMySqlModule),
               typeof(AutoMapperModule),
               typeof(CSRedisModule),
               typeof(MongoDBModelule)
        )]
    public class AppWebModule: AppModule
    {
        private string _corePolicyName = string.Empty;


        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddTransient(typeof(Lazy<>), typeof(LazyFactory<>));
            var configuration = context.GetConfiguration();
            context.Services.Configure<AppOptionSettings>(configuration.GetSection("Destiny"));
           
             var settings =context.GetConfiguration<AppOptionSettings>("Destiny");
             context.Services.AddObjectAccessor<AppOptionSettings>(settings);
            if (!settings.Cors.PolicyName.IsNullOrEmpty() && !settings.Cors.Url.IsNullOrEmpty()) //添加跨域
            {
                _corePolicyName = settings.Cors.PolicyName;
                context.Services.AddCors(c =>
                {
                    c.AddPolicy(settings.Cors.PolicyName, policy =>
                    {
                        policy.WithOrigins(settings.Cors.Url
                          .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        //policy.WithOrigins("http://localhost:5001")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();//允许cookie;
                    });
                });
            }
            context.Services.AddHttpContextAccessor();
            context.Services.AddControllers(o => {

                o.SuppressAsyncSuffixInActionNames = false;
                o.Filters.Add<PermissionAuthorizationFilter>();
                o.Filters.Add<AuditLogFilter>(); 
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });
            context.Services.AddTransient<IPrincipal>(provider =>
            {
                IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });

        }

        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            if (!_corePolicyName.IsNullOrEmpty())
            {
                app.UseCors(_corePolicyName); //添加跨域中间件
            }
            app.UseAuthentication(); //认证
            app.UseAuthorization();//授权
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

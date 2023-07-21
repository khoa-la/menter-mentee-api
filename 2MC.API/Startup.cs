using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using _2MC.API.AppStart;
using _2MC.API.Filters;
using _2MC.DataTier.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Reso.Core.Custom;
using Reso.Core.Extension;
using StackExchange.Redis;

namespace _2MC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureSwaggerServices();

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
                    // options.SuppressModelStateInvalidFilter = true
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState.Values;
                        throw new ErrorResponse(400, modelState.First().Errors.First().ErrorMessage);
                    };
                }
            );
            services.ConfigureDependencyInjection();
            services.ConfigureAutoMapper();
            services.ConfigureAuthServices(Configuration);
            services.ConfigureFilter<ErrorHandlingFilter>();
            services.AddHttpContextAccessor();
            services.AddDbContext<MentorMenteeConnectContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"), options =>
                    options.EnableRetryOnFailure())
            );

            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(Configuration["Endpoint:RedisEndpoint"]));
            services.ConfigMemoryCacheAndRedisCache(Configuration["Endpoint:RedisEndpoint"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //Firebase config
            var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "firebase.json");
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(pathToKey)
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SwaggerConfig.Configure(app, provider);

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            AuthConfig.Configure(app);

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
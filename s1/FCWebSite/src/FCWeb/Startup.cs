using Microsoft.Extensions.Configuration.UserSecrets;

[assembly: UserSecretsId("aspnet5-FCWeb-3e22889e-4966-4f45-932d-512f859d2b06")]
namespace FCWeb
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Core;
    using Core.Extensions;
    using Core.Extensions.Middleware;
    using FCCore.Configuration;
    using FCCore.Diagnostic.Logging.File;
    using FCDAL.Model;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.ResponseCaching;
    // using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Services;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            // Add framework services.
            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<FCDBContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], ob => ob.UseRowNumberForPaging()));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FCDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<LanguageActionFilter>();

            services.AddResponseCaching();

            services.AddMvc(options =>
            {
                var curJsonOutputFormatter = options.OutputFormatters.FirstOrDefault(of => of.GetType() == typeof(JsonOutputFormatter)) as JsonOutputFormatter;

                if (curJsonOutputFormatter != null)
                {
                    //curJsonOutputFormatter .SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                }

                //var jsonOutputFormatter = new JsonOutputFormatter();
                //jsonOutputFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                //jsonOutputFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                //jsonOutputFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;

                //options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                //options.OutputFormatters.Insert(0, jsonOutputFormatter);
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            });

            services.AddFCCache();
            services.AddCoreConfiguration(Configuration);
            // FCCache should be added after AddCoreConfiguration(...)            

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddBLLServices();
            services.AddDALServices();
            services.AddFCCoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddConsole();
            loggerFactory.AddTextFile(Path.Combine(Directory.GetCurrentDirectory(), "HookLog.txt"));
            //loggerFactory.AddAzureWebAppDiagnostics();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<FCWebContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            //app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseImageProcessingMiddleware();
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseIdentity();
            app.AddCoreConfiguration(Configuration);

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                // Set options here to change middleware behavior
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU")
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                   new CultureInfo("ru-RU")
                },
                DefaultRequestCulture = new RequestCulture("ru-RU")
            };

            app.UseRequestLocalization(requestLocalizationOptions);

            app.Use((context, next) =>
            {
                if (context.Features.Get<IResponseCachingFeature>() == null)
                {
                    context.Features.Set<IResponseCachingFeature>(new FakeResponseCachingFeature());
                }
                return next();
            });

            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "logoff",
                    template: "account/logoff",
                    defaults: new { controller = "account", action = "logoff" })
                .MapRoute(
                    name: "account",
                    template: "account/login",
                    defaults: new { controller = "account", action = "login" })
                .MapRoute(
                    name: "office",
                    template: "office/{*.}",
                    defaults: new { controller = "Office", action = "Index" })
                .MapRoute(
                    name: "default",
                    template: "{*.}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }

    public class FakeResponseCachingFeature : IResponseCachingFeature
    {
        public string[] VaryByQueryKeys
        {
            get;
            set;
        }
    }
}

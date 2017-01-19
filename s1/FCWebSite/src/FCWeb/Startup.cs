namespace FCWeb
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Core;
    using Core.Extensions;
    using Core.Extensions.Middleware;
    using FCCore.Configuration;
    using FCDAL.Model;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Formatters;
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
                builder.AddUserSecrets();
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
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FCDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<LanguageActionFilter>();

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

            services.AddCoreConfiguration(Configuration);

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
            loggerFactory.AddAzureWebAppDiagnostics();

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

            app.UseStaticFiles();

            app.UseIdentity();

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

            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "account",
                    template: "account/{*.}",
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

        //// Entry point for the application.
        //public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

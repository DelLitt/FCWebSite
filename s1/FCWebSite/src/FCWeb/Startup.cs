namespace FCWeb
{
    using System.Collections.Generic;
    using System.Globalization;
    using Core;
    using Core.Extensions;
    using FCCore.Configuration;
    using FCDAL.Model;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Localization;
    using Microsoft.Data.Entity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Services;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
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
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<FCDBContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FCDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<LanguageActionFilter>();

            services.AddMvc();
            services.AddCoreConfiguration(Configuration);

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddBLLServices();
            services.AddDALServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

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
                }
            };

            app.UseRequestLocalization(requestLocalizationOptions, defaultRequestCulture: new RequestCulture("ru-RU"));

            app.UseMvc(routes =>
            {
                routes
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

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

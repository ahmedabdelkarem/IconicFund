using AutoMapper;
//using DevExpress.AspNetCore;
//using DevExpress.AspNetCore.Reporting;
using IconicFund.Context;
using IconicFund.Helpers;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Services.IServices;
using IconicFund.Services.Services;
using IconicFund.Web.Core;
using IconicFund.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace IconicFund.Web
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
            //services.AddDevExpressControls();
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            //services.ConfigureReportingServices(configurator => {
            //    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
            //        viewerConfigurator.UseCachedReportSourceBuilder();
            //    });
            //});

            services.AddDbContext<IconicFundDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //AUTHENTICATIONS
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.Cookie.Name = "IOAdminAuthCookie";
            });

            services.AddControllersWithViews();

            //EMBDED Services
            services.AddHttpContextAccessor();
            services.AddSession(opts =>
            {
                opts.Cookie.IsEssential = true; // make the session cookie Essential
            });

            services.AddAutoMapper(typeof(Startup));

            //services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            //object p = services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IBaseRepository, BaseRepository>();            
            services.AddTransient<INationalityService, NationalityService>();            
            services.AddTransient<IAttachmentService, AttachmentService>();            
            services.AddTransient<IHasherService, HasherService>();
            services.AddTransient<IAdminsService, AdminsService>();
            services.AddTransient<ISessionService, SessionService>();
           services.AddTransient<IPermissionsGroupService, PermissionsGroupService>();
            services.AddTransient<IPermissionGroupAdminService, PermissionGroupAdminService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<IBasicSystemSettingService, BasicSystemSettingService>();
            services.AddTransient<IDateTypesService, DateTypesService>();
            services.AddTransient<IPasswordComplexityService, PasswordComplexityService>();
            services.AddTransient<IMailService, MailService>();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IconicFundDbContext context, IHasherService hasherService)
        {
            //app.UseDevExpressControls();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("AR")),
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("AR")
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("AR")
                }
            });

            //===== Initialize and Seed DB =======//
            InitializeDatabase(app);
            Seed(context, env, hasherService).Wait();
            //=====================================//

            //Create needed direcotries for uploaded files
            FoldersConfig.RegisterAll(env);

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region Initialize and Seed DB

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<IconicFundDbContext>().Database.Migrate();
            }
        }

        private async Task Seed(IconicFundDbContext context, IWebHostEnvironment _hostingEnvironment, IHasherService hasherService)
        {
            if (!await context.Roles.AnyAsync())
            {
                context.Roles.Add(new Role { Id = Guid.Parse(Constants.MainAdminRoleId), Name = "مدير النظام" });
                //context.Roles.Add(new Role { Id = Guid.Parse(Constants.SaftyRoleId), Name = "السلامة" });
                //context.Roles.Add(new Role { Id = Guid.Parse(Constants.LiftsRoleId), Name = "المصاعد" });
                //context.Roles.Add(new Role { Id = Guid.Parse(Constants.EngineeringOfficeRoleId), Name = "المكاتب الهندسية" });

                await context.SaveChangesAsync();
            }

            if (!await context.Admins.AnyAsync())
            {
                context.Admins.Add(new Admin
                {
                    Id = Guid.Parse(Constants.MainAdminId),
                    FirstName = "مدير",
                    LastName = "النظام",
                    MobileNumber = "567123432",
                    Email = "Abdullah@apit.net.sa", 
                    EmplyeeNo = "1",
                    NationalId = "1",
                    Password = hasherService.ComputeSha256Hash("mainadmin"),
                    CanApprove = true,

                    IsActive = true,
                    ActivationStartDate = DateTime.Now,
                    ActivationEndDate = null,

                    Roles = new List<AdminRole> { new AdminRole { RoleId = Guid.Parse(Constants.MainAdminRoleId) } }
                });

                await context.SaveChangesAsync();
            }

            //if (!context.SaftyOffices.Any())
            //{
            //    context.SaftyOffices.Add(new SaftyOffice { Name = "مركز السلامة الميدانية بالمنطقة المركزية " });
            //    context.SaftyOffices.Add(new SaftyOffice { Name = "مركز السلامة الميدانية بالشوقية" });
            //    context.SaftyOffices.Add(new SaftyOffice { Name = "مركز السلامة الميدانية بالعزيزية" });
            //    context.SaftyOffices.Add(new SaftyOffice { Name = "مركز السلامة الميدانية بالمعابدة" });
            //    context.SaftyOffices.Add(new SaftyOffice { Name = "مركز السلامة الميدانية بالشرايع" });

            //    await context.SaveChangesAsync();
            //}

            if (!context.Nationalities.Any() && !context.Cities.Any() && !context.Regions.Any())
            {
                var seedPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Seed");
                var CitiesJSONtxt = File.ReadAllText(Path.Combine(seedPath, "Cities.json"));
                var NationalitiesJSONtxt = File.ReadAllText(Path.Combine(seedPath, "Nationalities.json"));
                var RegionsJSONtxt = File.ReadAllText(Path.Combine(seedPath, "Regions.json"));

                var Cities = JsonConvert.DeserializeObject<CityViewModels>(CitiesJSONtxt);
                var Nationalities = JsonConvert.DeserializeObject<NationalitiesViewModel>(NationalitiesJSONtxt);
                var Regions = JsonConvert.DeserializeObject<RegionsViewModel>(RegionsJSONtxt);
                context.Database.OpenConnection();
                try
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cities ON");
                    context.Cities.AddRange(Cities.Cities);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Cities OFF");


                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Regions ON");
                    context.Regions.AddRange(Regions.Regions);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Regions OFF");


                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Nationalities ON");
                    context.Nationalities.AddRange(Nationalities.Nationalities);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Nationalities OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }

                //context.Cities.AddRange(Cities.Cities);
                //context.Nationalities.AddRange(Nationalities.Nationalities);
                //context.Regions.AddRange(Regions.Regions);

                //await context.SaveChangesAsync();
            }
        }


        #endregion

    }
}

using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using AspNetCoreDashboardBackend.Configuration;

namespace AspNetCoreDashboardBackend;

public class Startup {
    public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) {
        Configuration = configuration;
        FileProvider = hostingEnvironment.ContentRootFileProvider;
        DashboardExportSettings.CompatibilityMode = DashboardExportCompatibilityMode.Restricted;            
    }

    public IFileProvider FileProvider { get; }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        // Configures CORS policies.                
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.WithHeaders("Content-Type");
            });
        });
        services.AddMvc();
        services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
            DashboardConfigurator configurator = new DashboardConfigurator();

            // Create and configure a dashboard storage.
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(FileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
            configurator.SetDashboardStorage(dashboardFileStorage);

            // Create and configure a data source storage.
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            
            ObjectDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage);

            configurator.SetDataSourceStorage(dataSourceStorage);

            // Uncomment the next line to allow users to create new data sources based on predefined connection strings.
            configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(Configuration));

            return configurator;
        });

        // Adds the DevExpress middleware.
        services.AddDevExpressControls();

        // Adds controllers.
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        // Registers the DevExpress middleware.
        app.UseDevExpressControls();

        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Home/Error");
        }

        // Registers routing.
        app.UseRouting();

        // Registers CORS policies.
        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints => {
            // Map dashboard routes.
            endpoints.MapDashboardRoute("api/dashboard", "DefaultDashboard");

            // Requires CORS policies.
            endpoints.MapControllers().RequireCors("CorsPolicy");
        });
    }
}

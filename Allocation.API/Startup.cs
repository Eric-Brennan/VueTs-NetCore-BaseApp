using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allocation.API.Repository;
using Allocation.API.Repository.Interfaces;
using Allocation_API.Repository;
using Allocation_API.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AllocationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerDocument(settings =>
            {
                settings.Title = "AllocationAPI";
            });

            services.AddControllers();

            services.AddScoped<IDataAccess, DataAccess>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();



            app.UseOpenApi();

            app.UseSwaggerUi3(settings =>
            {
                settings.DocumentTitle = "AllocationAPI";
            });

            app.UseReDoc(config =>
            {
                config.Path = "/redoc";
                config.DocumentPath = "swagger/v1/swagger.json";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

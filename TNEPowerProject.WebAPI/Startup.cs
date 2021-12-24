using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Logics.Services;

namespace TNEPowerProject.WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string energoDBConnectionString = Configuration.GetConnectionString("EnergoDBConnectionString");
            services.AddDbContext<EnergoDBContext>(options => options.UseSqlServer(energoDBConnectionString));
            services.AddScoped<ITransformerTypesService, TransformerTypesService>();
            services.AddScoped<IElectricEnergyMeterTypesService, ElectricEnergyMeterTypesService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TransNeftEnergo API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello and welcome to TransNeftEnergoApi! Use /swagger to enter API.\nBest regards,\nDev Team");
                });
            });
        }
    }
}

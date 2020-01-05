using System;
using AbstractProject.BusinessLogic.Items.Queries.Items.GetItems;
using AbstractProject.BusinessLogic.Items.Queries.Items.GetItems.Profiles;
using AbstractProject.DataAccess;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AbstractProject.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(paramName: nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString(name: "Default");

            if (string.IsNullOrWhiteSpace(value: connectionString))
                throw new ArgumentException(
                    message: "Value cannot be null or whitespace.",
                    paramName: nameof(connectionString));

            services.AddDbContext<AbstractProjectDbContext>(optionsAction: options =>
                options.UseInMemoryDatabase(databaseName: connectionString));

            services.AddMemoryCache();
            services.AddResponseCompression();

            services.AddMediatR(typeof(GetItemsQuery));
            services.AddAutoMapper(typeof(ItemsProfile));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseResponseCompression();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints => { endpoints.MapControllers(); });
        }
    }
}
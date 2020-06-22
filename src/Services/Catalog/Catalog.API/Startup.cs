using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Catalog.API.Models;
using eShopOnContainers.Common.EventBus;
using eShopOnContainers.Common.EventBus.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalog.API
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
            services.AddTransient<ICatalogRepo>(s => new CatalogRepo(Configuration.GetConnectionString("Catalog")));
            
            services.AddSingleton<IEventBus>(s => new RabbitMQEventBus(Configuration.GetValue<string>("EventBus:Rabbit"), "checkout1"));
            services.AddSingleton<ISubscriber<Checkout>, StockUpdater>();

            services.AddControllers();
            services.AddSwaggerGen(
                c => {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "CatalogAPI",
                        Version = "v1"
                    });
                    c.IncludeXmlComments(Path.Combine(
                        AppContext.BaseDirectory,
                        $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml"
                    ));
                }

            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetService<ISubscriber<Checkout>>();

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CatalogAPI v1");
                }
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Basket.API.Models;
using Basket.API.Services;
using eShopOnContainers.Common.EventBus;
using eShopOnContainers.Common.EventBus.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Basket.API
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
            services.AddSingleton<IEventBus>(s => new RabbitMQEventBus(
                Configuration.GetValue<string>("EventBus:Rabbit"),
                "checkout1"
            ));
            
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var connectionString = Configuration.GetConnectionString("Basket");
                var configuration = ConfigurationOptions.Parse(connectionString, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddSingleton<ICatalogService>(sp => new CatalogAPIClient(
                Configuration.GetValue<string>("Services:Catalog.API"), 
                new HttpClient()
            ));
            services.AddTransient<IBasketRepository, RedisBasketRepository>();
            services.AddControllers();
            services.AddSwaggerGen(
                c => {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "BasketAPI",
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
            
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketAPI v1");
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

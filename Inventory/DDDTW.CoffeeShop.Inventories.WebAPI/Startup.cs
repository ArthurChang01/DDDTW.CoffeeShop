using DDDTW.CoffeeShop.Inventories.WebAPI.Middlewares;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace DDDTW.CoffeeShop.Inventories.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptionObjectService(this.Configuration);

            services.AddMvcService();

            services.AddSwaggerService();

            return services.SetIoCService(this.Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGlobalExceptionHandler(x =>
            {
                x.ContentType = "application/json";
                x.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    s.Message
                }));
            });

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseSwaggerService();

            app.UseMvcService();
        }
    }
}
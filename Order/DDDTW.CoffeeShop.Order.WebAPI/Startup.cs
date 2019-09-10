using Autofac;
using Autofac.Extensions.DependencyInjection;
using DDDTW.CoffeeShop.Order.Application;
using FluentValidation.AspNetCore;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace DDDTW.CoffeeShop.Order.WebAPI
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
            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "Inventory API", Version = "v1" });
            });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddMediatR(typeof(OrderApplication).Assembly);

            OrderApplication.Load(containerBuilder, this.Configuration);
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });

            app.UseMvc();
        }
    }
}
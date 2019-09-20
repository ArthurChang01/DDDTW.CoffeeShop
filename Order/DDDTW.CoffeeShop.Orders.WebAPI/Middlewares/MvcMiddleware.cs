using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Middlewares
{
    public static class MvcMiddleware
    {
        public static void AddMvcService(this IServiceCollection services)
        {
            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                });
        }

        public static void UseMvcService(this IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
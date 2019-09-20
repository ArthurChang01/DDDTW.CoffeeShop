using DDDTW.CoffeeShop.Infrastructures.Repositories.EventSourcings;
using DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Middlewares
{
    public static class OptionMiddleware
    {
        public static void AddOptionObjectService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ESConfig>(config.GetSection("Databases:EventSourcing"));
            services.Configure<MongoConfig>(config.GetSection("Databases:MongoDb"));
        }
    }
}
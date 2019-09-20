using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using MongoDB.Bson.Serialization;

namespace DDDTW.CoffeeShop.Orders.Application.Products.Repositories
{
    public class ProductDbMapper : IMongoClassMappingRegister<Product>
    {
        private readonly ITranslator<ProductId, string> idTranslator;

        public ProductDbMapper(ITranslator<ProductId, string> idTranslator)
        {
            this.idTranslator = idTranslator;
        }

        public void RegisterClass()
        {
            BsonSerializer.RegisterSerializer<ProductId>(
                new EntityIdSerializer<ProductId>(value => this.idTranslator.Translate(value)));

            if (BsonClassMap.IsClassMapRegistered(typeof(ProductId)) == false)
                BsonClassMap.RegisterClassMap<ProductId>();

            if (BsonClassMap.IsClassMapRegistered(typeof(Product)) == false)
                BsonClassMap.RegisterClassMap<Product>();
        }
    }
}
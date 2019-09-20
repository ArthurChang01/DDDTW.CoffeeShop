using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Products.Applications
{
    public class GetProductBySvc : IRequestHandler<GetProductByMsg, ProductRst>
    {
        private readonly ITranslator<ProductId, string> idTranslator;
        private readonly IProductRepository repository;

        public GetProductBySvc(
            ITranslator<ProductId, string> idTranslator,
            IProductRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public async Task<ProductRst> Handle(GetProductByMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.ProductId);
            var product = await this.repository.GetBy(id);

            return new ProductRst(product);
        }
    }
}
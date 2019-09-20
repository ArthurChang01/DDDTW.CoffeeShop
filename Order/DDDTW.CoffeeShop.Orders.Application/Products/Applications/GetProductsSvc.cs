using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Products.Applications
{
    public class GetProductsSvc : IRequestHandler<GetProductMsg, IEnumerable<ProductRst>>
    {
        private readonly IProductRepository repository;

        public GetProductsSvc(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ProductRst>> Handle(GetProductMsg request, CancellationToken cancellationToken)
        {
            var products = await this.repository.Get(request.PageNo, request.PageSize);

            return products.Select(o => new ProductRst(o));
        }
    }
}
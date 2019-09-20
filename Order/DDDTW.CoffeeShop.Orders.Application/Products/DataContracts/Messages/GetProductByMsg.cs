using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using MediatR;

namespace DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages
{
    public class GetProductByMsg : IRequest<ProductRst>
    {
        public GetProductByMsg()
        {
        }

        public GetProductByMsg(string productId)
        {
            this.ProductId = productId;
        }

        public string ProductId { get; set; }
    }
}
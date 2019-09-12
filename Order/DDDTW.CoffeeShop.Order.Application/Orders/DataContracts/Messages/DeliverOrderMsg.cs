using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class DeliverOrderMsg : IRequest<Unit>
    {
        public DeliverOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}
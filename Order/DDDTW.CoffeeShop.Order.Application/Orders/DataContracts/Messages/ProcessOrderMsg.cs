using MediatR;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class ProcessOrderMsg : IRequest<Unit>
    {
        public ProcessOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}
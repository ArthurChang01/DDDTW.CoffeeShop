using MediatR;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class CloseOrderMsg : IRequest<Unit>
    {
        public CloseOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DomainServices
{
    public class OrderVMTranslator : ITranslator<OrderVM, Domain.Orders.Models.Order>
    {
        public OrderVM Translate(Domain.Orders.Models.Order input)
        {
            return new OrderVM()
            {
                Id = input.ToString() ?? string.Empty,
                Status = input.Status,
                Items = input.OrderItems,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate
            };
        }
    }
}
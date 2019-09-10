using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using System.Linq;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DomainServices
{
    public class OrderVMTranslator : ITranslator<OrderVM, Domain.Orders.Models.Order>
    {
        public OrderVM Translate(Domain.Orders.Models.Order input)
        {
            var items = input.OrderItems.Select(o => new OrderItemVM(o));
            return new OrderVM()
            {
                Id = input?.ToString() ?? string.Empty,
                Status = input.Status.ToString(),
                Items = items,
                CreatedDate = input.CreatedDate,
                ModifiedDate = input.ModifiedDate
            };
        }
    }
}
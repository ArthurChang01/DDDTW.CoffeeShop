using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests;
using FluentValidation;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Validators.Orders
{
    public class ChangeStatusValidator : AbstractValidator<ChangeStatusReq>
    {
        public ChangeStatusValidator()
        {
            RuleFor(x => x.OrderStatus)
                .NotEqual(OrderStatus.Initial);
        }
    }
}
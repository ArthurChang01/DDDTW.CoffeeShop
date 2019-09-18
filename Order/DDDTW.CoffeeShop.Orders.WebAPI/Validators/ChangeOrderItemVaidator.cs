using DDDTW.CoffeeShop.Orders.WebAPI.Models.Requests;
using FluentValidation;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Validators
{
    public class ChangeOrderItemValidator : AbstractValidator<ChangeOrderItemReq>
    {
        public ChangeOrderItemValidator()
        {
            RuleFor(x => x.OrderItems)
                .NotNull()
                .NotEmpty();
        }
    }
}
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Requests;
using FluentValidation;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Validators
{
    public class AddOrderValidator : AbstractValidator<AddOrderReq>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotNull()
                .NotEmpty();
        }
    }
}
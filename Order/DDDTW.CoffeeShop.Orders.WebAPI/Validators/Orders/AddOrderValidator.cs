using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace DDDTW.CoffeeShop.Orders.WebAPI.Validators.Orders
{
    public class AddOrderValidator : AbstractValidator<AddOrderReq>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotNull()
                .NotEmpty()
                .Custom((req, ctx) =>
                {
                    for (int i = 0; i < req.Count(); i++)
                    {
                        var rm = req.ElementAt(i);
                        if (Regex.IsMatch(rm.ProductId, @"\S{3}-\d{8}-\d+"))
                            ctx.AddFailure($"Item[{i}].ProductId format is incorrect");

                        if (rm.Price < 0)
                            ctx.AddFailure($"item[{i}].Price can not less than 0");

                        if (rm.Qty < 0)
                            ctx.AddFailure($"item[{i}].Qty can not less than 0");
                    }
                });
        }
    }
}
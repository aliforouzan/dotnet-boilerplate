using FluentValidation;
using DotnetBoilerPlate.Application.Dto.Orders.Delete;

namespace DotnetBoilerPlate.Application.Filters.req.Validators.Orders;

public class OrderDeleteValidator : AbstractValidator<OrderDeleteRequestDto>
{

    public OrderDeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
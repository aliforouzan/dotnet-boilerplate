using FluentValidation;
using DotnetBoilerPlate.Application.Dto.Orders.Update;
using DotnetBoilerPlate.Shared.Statics;

namespace DotnetBoilerPlate.Application.Filters.req.Validators.Orders;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateRequestDto>
{
    public OrderUpdateValidator()
    {

        /*RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Individuality)
            .NotEmpty();

        RuleFor(x => x.HeroType)
            .IsInEnum();

        RuleFor(x => x.Age)
            .GreaterThan(0);
        
        RuleFor(x => x.Nickname)
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Team)
            .MaximumLength(StringSizes.Max);*/
    }
}
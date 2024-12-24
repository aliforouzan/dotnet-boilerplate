using FluentValidation;
using DotnetBoilerPlate.Application.Dto.Orders.Create;

namespace DotnetBoilerPlate.Application.Filters.req.Validators.Orders;

public class OrderCreateValidator : AbstractValidator<OrderCreateRequestDto>
{
    public OrderCreateValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("کالای ارسال شده جهت سفارش معتبر نیست");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("عملیات سفارش معتبر نیست");

        RuleFor(x => x.UnitCount)
            .GreaterThan(0)
            .WithMessage("حداقل تعداد در سفارش معتبر نیست")
            .Must(x => (x % 1.0M) == 0)
            .WithMessage("تعداد سفارش باید عددی صحیح باشد");

        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0)
            .WithMessage("حداقل قیمت برای واحد معتبر نیست")
            .Must(x => (x % 1.0M) == 0)
            .WithMessage("قیمت باید عددی صحیح باشد");
    }
}
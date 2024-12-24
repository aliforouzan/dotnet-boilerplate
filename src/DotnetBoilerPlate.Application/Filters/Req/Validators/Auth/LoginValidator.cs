using FluentValidation;
using DotnetBoilerPlate.Application.Dto.Auth.Login;
using DotnetBoilerPlate.Shared.Statics;

namespace DotnetBoilerPlate.Application.Filters.Req.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("ارسال نام کاربری الزامی است")
            .Length(10, 11)
            .WithMessage("نام کاربری ارسالی نامعتبر است");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("ارسال کلمه عبور الزامی است")
            .MaximumLength(StringSizes.Max);
    }
}
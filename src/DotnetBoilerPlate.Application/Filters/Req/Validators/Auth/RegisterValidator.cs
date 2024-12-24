using FluentValidation;
using DotnetBoilerPlate.Application.Dto.Auth.Register;

namespace DotnetBoilerPlate.Application.Filters.Req.Validators.Auth;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Firstname)
            .NotEmpty()
            .WithMessage("ارسال نام الزامی است")
            .MaximumLength(50)
            .WithMessage("طول نام بیش از حد مجاز است");

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .WithMessage("ارسال نام خانوادگی الزامی است")
            .MaximumLength(100)
            .WithMessage("طول نام خانوادگی بیش از حد مجاز است");

        RuleFor(x => x.NationalCode)
            .NotEmpty()
            .WithMessage("ارسال کد ملی الزامی است")
            .Must(x => x.IsValid())
            .WithMessage("کد ملی معتبر نیست");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("ارسال شماره همراه الزامی است")
            .Matches(@"^09\d{9}$")
            .WithMessage("شماره همراه معتبر نیست");
        
        RuleFor(x => x.Email)
            .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
            .WithMessage("ایمیل معتبر نیست");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("ارسال پسورد الزامی است")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
            )
            .WithMessage("حداقل طول پسورد باید ۸ کاراکتر و حاوی حداقل یک حرف کوچک، یک حرف بزرگ و یک عدد و یک علامت خاص باشد");
        
        RuleFor(x => x.PostalCode)
            .Matches(@"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b")
            .WithMessage("کد پستی معتبر نیست");
    }
}
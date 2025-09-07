using System;
using FluentValidation;
using UserPortal.UseCases.DTOs;

namespace UserPortal.UseCases.Validations.DTOs;

public class LoginDtoValidations : AbstractValidator<LoginDto>
{
    public LoginDtoValidations()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("آدرس ایمیل الزامی می‌باشد.")
            .EmailAddress()
            .WithMessage("آدرس ایمیل نامعتبر است.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور الزامی است.")
            .NotEmpty()
            .WithMessage("رمز عبور الزامیست.")
            .Length(6, 120)
            .WithMessage("رمز عبور می‌بایست بین 6 تا 120 کاراکتر باشد!")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])")
            .WithMessage("رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.");
    }
}

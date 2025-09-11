using System;
using FluentValidation;
using UserPortal.UseCases.DTOs;

namespace UserPortal.UseCases.Validations.DTOs;

public class RegistreDtoValidations : AbstractValidator<RegisterDto>
{
    public RegistreDtoValidations()
    {
        // UserName
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("نام کاربری الزامیست.")
            .Length(3, 50)
            .WithMessage("نام کاربری باید حداقل 3 کاراکتر و حداکثر 50 کاراکتر باشد.")
            .Matches(@"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
            .WithMessage("نام کاربری فقط می‌تواند شامل حروف انگلیسی، اعداد، نقطه و زیرخط باشد. نمی‌تواند با نقطه یا زیرخط شروع یا پایان یابد و نمی‌تواند شامل دو نقطه یا دو زیرخط پشت سر هم باشد.")
            .NotEqual("Anonymous")
            .NotEqual("Unknown")
            .WithMessage("استفاده از این نام کاربری غیر مجاز می‌باشد.");

        // Email
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("آدرس ایمیل الزامی می‌باشد.")
            .EmailAddress()
            .WithMessage("آدرس ایمیل نامعتبر است.");

        // Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور الزامیست.")
            .Length(6, 120)
            .WithMessage("رمز عبور می‌بایست بین 6 تا 120 کاراکتر باشد!")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])")
            .WithMessage("رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.");
    }
}

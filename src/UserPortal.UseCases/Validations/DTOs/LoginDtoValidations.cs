using System;
using FluentValidation;
using UserPortal.Core.Constants;
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
            .Length(ValidationConstants.Password.MinLength,
                    ValidationConstants.Password.MaxLength)
            .WithMessage(
                ValidationMessages.ValidLengthRange(
                    "رمز عبور",
                    ValidationConstants.Password.MinLength,
                    ValidationConstants.Password.MaxLength
                )
            )
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])")
            .WithMessage("رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.");
    }
}

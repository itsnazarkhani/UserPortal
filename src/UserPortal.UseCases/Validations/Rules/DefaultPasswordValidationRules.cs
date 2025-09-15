using System;
using UserPortal.Core.Constants;

namespace UserPortal.UseCases.Validations.Rules;

public class DefaultPasswordValidationRules : IPasswordValidationRules
{
    public int MinLength => ValidationConstants.Password.MinLength;
    public int MaxLength => ValidationConstants.Password.MaxLength;
    public string Pattern => @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])";
    public string EmptyMessage => "رمز عبور الزامیست.";
    public string LengthMessage => ValidationMessages.ValidLengthRange(
        "رمز عبور",
        MinLength,
        MaxLength
    );
    public string PatternMessage => "رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.";
}
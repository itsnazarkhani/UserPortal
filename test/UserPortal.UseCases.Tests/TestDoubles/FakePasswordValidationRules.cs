using System;
using UserPortal.Core.Constants;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Tests.TestDoubles;

public class FakePasswordValidationRules : IPasswordValidationRules
{
    public int MinLength => ValidationConstants.Password.MinLength;
    public int MaxLength => ValidationConstants.Password.MaxLength;
    public string Pattern => @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#!$%^&*?])";
    public string EmptyMessage => "رمز عبور الزامیست.";
    public string LengthMessage => $"رمز عبور باید بین {MinLength} و {MaxLength} کاراکتر باشد.";
    public string PatternMessage => "رمز عبور میبایست حداقل دارای یک عدد، یک حرف انگلیسی کوچک و یک حرف انگلیسی بزرگ و یکی از کاراکترهای خاصی همچون \"@#!$%^&*?\" باشد.";
}
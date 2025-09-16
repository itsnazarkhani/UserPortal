using System;
using UserPortal.Core.Constants;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Tests.TestDoubles;

public class FakeUserNameValidationRules : IUserNameValidationRules
{
    public int MinLength => ValidationConstants.UserName.MinLength;

    public int MaxLength => ValidationConstants.UserName.MaxLength;

    public string Pattern => @"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";

    public string EmptyMessage => "نام کاربری الزامیست.";

    public string LengthMessage => $"نام کاربری باید بین {MinLength} و {MaxLength} کاراکتر باشد.";

    public string PatternMessage => "نام کاربری فقط می‌تواند شامل حروف انگلیسی، اعداد، نقطه و زیرخط باشد. نمی‌تواند با نقطه یا زیرخط شروع یا پایان یابد و نمی‌تواند شامل دو نقطه یا دو زیرخط پشت سر هم باشد.";

    public string[] InvalidUserNames => ["Anonymous", "Unknown"];

    public string InvalidUserNameMessage => "استفاده از این نام کاربری غیر مجاز می‌باشد.";
}

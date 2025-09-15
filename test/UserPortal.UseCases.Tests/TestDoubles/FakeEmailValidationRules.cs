using System;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Tests.TestDoubles;

public class FakeEmailValidationRules : IEmailValidationRules
{
    public string EmptyMessage => "آدرس ایمیل الزامی می‌باشد.";
    public string InvalidMessage => "آدرس ایمیل نامعتبر است.";
}
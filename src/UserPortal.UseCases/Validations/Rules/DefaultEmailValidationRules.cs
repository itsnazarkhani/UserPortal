using System;

namespace UserPortal.UseCases.Validations.Rules;

public class DefaultEmailValidationRules : IEmailValidationRules
{
    public string EmptyMessage => "آدرس ایمیل الزامی می‌باشد.";
    public string InvalidMessage => "آدرس ایمیل نامعتبر است.";
}
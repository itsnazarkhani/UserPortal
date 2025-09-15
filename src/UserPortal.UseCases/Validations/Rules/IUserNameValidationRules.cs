using System;

namespace UserPortal.UseCases.Validations.Rules;

public interface IUserNameValidationRules
{
    int MinLength { get; }
    int MaxLength { get; }
    string Pattern { get; }
    string EmptyMessage { get; }
    string LengthMessage { get; }
    string PatternMessage { get; }
    string[] InvalidUserNames { get; }
    string InvalidUserNameMessage { get; }
}
